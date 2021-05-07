using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using ServiceAdaptor.NetCore.Client;
using System;
using System.Threading.Tasks;
using Vit.Core.Module.Log;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.Consul
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceAdaptor : IServiceAdaptor
    {
        //参考 https://www.jianshu.com/p/4aaaee6e9ce1

        #region config Model
        internal class ConsulConfig
        {
            /// <summary>
            /// consul的地址。如 http://127.0.0.1:8500
            /// </summary>
            public string ConsulEndpoint;

            /// <summary>
            /// 提供的服务的地址，如 127.0.0.1、sers.cloud
            /// </summary>
            public string serviceHost;
            /// <summary>
            /// 提供的服务的端口号
            /// </summary>
            public int servicePort;
            /// <summary>
            /// 提供的服务的名称，如 ServiceProvider
            /// </summary>
            public string serviceName;

            public string serviceId => $"ID_{serviceHost}:{servicePort}/{serviceName}";
            public string healthCheckUrl => "/_Consul_/HealthCheck";
        }
        #endregion




        public IWebHostBuilder InitWebHostBuilder(IWebHostBuilder builder, JObject config, out IApiClient apiClient)
        {
            var consulConfig = config.ConvertBySerialize<ConsulConfig>();

            var httpApi = new ApiClient(consulConfig.ConsulEndpoint);
            apiClient = httpApi;


            #region 超时时间
            var timeoutSeconds = config["timeoutSeconds"].ConvertBySerialize<int?>();
            if (timeoutSeconds.HasValue)
            {
                httpApi.vitHttpClient.TimeoutSeconds = timeoutSeconds.Value;
            }
            #endregion


            builder.ConfigureServices((WebHostBuilderContext context, IServiceCollection services) =>
            {
                services.AddSingleton<IConsulClient>(sp => new global::Consul.ConsulClient(c =>
                {
                    c.Address = new Uri(consulConfig.ConsulEndpoint);
                }));

            }).ConfigureApp(app =>
            {
                Logger.Info("[ServiceAdaptor.NetCore.Consul]注册... ");

                Logger.Info("[ServiceAdaptor.NetCore.Consul]配置：" + consulConfig.Serialize());

                #region Configure

                #region (x.1) 注册Consul服务         

                var serviceProvider = app.ApplicationServices;

                //(x.x.1)向Consul注册服务
                IConsulClient consul = serviceProvider.GetRequiredService<IConsulClient>();
                IApplicationLifetime appLife = serviceProvider.GetRequiredService<IApplicationLifetime>();

                var registration = new AgentServiceRegistration()
                {
                    Checks = new[] {
                        new AgentServiceCheck()
                        {
                            DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                            Interval = TimeSpan.FromSeconds(30),
                            HTTP = $"{Uri.UriSchemeHttp}://{consulConfig.serviceHost}:{consulConfig.servicePort}{consulConfig.healthCheckUrl}",
                        }
                    },
                    Address = consulConfig.serviceHost,
                    ID = consulConfig.serviceId,
                    Name = consulConfig.serviceName,
                    Port = consulConfig.servicePort
                };
                try
                {
                    var result = consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

                    if ((result?.StatusCode) != System.Net.HttpStatusCode.OK)
                    {
                        Logger.Info("[ServiceAdaptor.NetCore.Consul]注册失败!");
                        Task.Run(() =>
                        {
                            appLife.StopApplication();
                        });
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);

                    Logger.Info("[ServiceAdaptor.NetCore.Consul]注册失败!");
                    Task.Run(() =>
                    {
                        appLife.StopApplication();
                    });
                    return;
                    //throw;
                }



                //(x.x.2)send consul request after service stop
                appLife.ApplicationStopping.Register(() =>
                {
                    consul.Agent.ServiceDeregister(consulConfig.serviceId).GetAwaiter().GetResult();
                });
                #endregion



                #region (x.2) 添加健康检查接口              
                app.Map(consulConfig.healthCheckUrl, s =>
                {
                    s.Run(async context =>
                    {
                        await context.Response.WriteAsync("ok");
                    });
                });
                #endregion



                #endregion

                Logger.Info("[ServiceAdaptor.NetCore.Consul]注册成功!");

            });

            return builder;
        }
    }
}
