using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using ServiceAdaptor.NetCore.Client;
using Steeltoe.Discovery.Client;
using Vit.Core.Module.Log;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.Be.Eureka
{
    public class ServiceAdaptor : IServiceAdaptor
    {
        public IWebHostBuilder InitWebHostBuilder(IWebHostBuilder builder, JObject config, out IApiClient apiClient)
        {
            var httpApi = new ApiClient();
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

                var env = services.BuildServiceProvider().GetService<IHostingEnvironment>();
                var configBuilder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                  .AddJsonFile("appsettings.json")
                  .AddEnvironmentVariables();
                var Configuration = configBuilder.Build();

                //services.AddBlueearthProvider(Configuration);          
                //services.AddBlueearthProvider(Configuration, false, false, true);
                services.AddBlueearthProvider(Configuration, false, false, false);

                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            }).AddConfigure(app=> 
            {              
                app.UseDiscoveryClient();
            });    


            return builder;
        }
    }
}
