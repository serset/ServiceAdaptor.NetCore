using System;
using Microsoft.AspNetCore.Hosting;
using Vit.Core.Util.ConfigurationManager;
using Newtonsoft.Json.Linq;
using Vit.Core.Module.Log;
using ServiceAdaptor.NetCore.Client;
using ServiceAdaptor.NetCore;
using Vit.Core.Util.Reflection;

namespace Vit.Extensions
{
    public static class IWebHostBuilderExtensions_UseServiceAdaptor
    {
        
        public static IWebHostBuilder UseServiceAdaptor(this IWebHostBuilder hostBuilder)
        {
            var configs = ConfigurationManager.Instance.GetByPath<JArray>("ServiceAdaptor");
            if (configs  == null|| configs.Count==0)
            {
                return hostBuilder;
            }

            foreach (JObject msConfig in configs)
            {
                try
                {
                    //(x.1) GetInstance
                    IServiceAdaptor msAdaptor = GetInstance(msConfig);
                    if (msAdaptor == null) continue;


                    //(x.2) init
                    Logger.Info("[ServiceAdaptor.NetCore]启用微服务适配器："+ msAdaptor.GetType().FullName);
                    var builder = msAdaptor.InitWebHostBuilder(hostBuilder, msConfig, out var apiClient);
                    ApiClient.Instance = apiClient;

                    return builder;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);                  
                }
            }

            return hostBuilder;


            #region GetInstance
            IServiceAdaptor GetInstance(JObject config)
            {
                //(x.1) get className    
                var className = config["className"].ConvertToString();
                if (string.IsNullOrEmpty(className)) return null;

                var assemblyFile = config["assemblyFile"].ConvertToString();
                if (string.IsNullOrEmpty(assemblyFile)) return null;
 

                //(x.2)get assembly 
                var assembly = ObjectLoader.LoadAssemblyFromFile(assemblyFile);              

                //(x.3) create class
                return assembly?.CreateInstance(className) as IServiceAdaptor;
            }
            #endregion
        }





    
    }
}
