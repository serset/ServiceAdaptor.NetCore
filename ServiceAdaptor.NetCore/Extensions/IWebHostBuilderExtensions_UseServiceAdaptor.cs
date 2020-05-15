 

using System;
using Microsoft.AspNetCore.Hosting;
using Vit.Core.Util.ConfigurationManager;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Vit.Core.Util.Common;
using System.IO;
using System.Linq;
using Vit.Core.Module.Log;
using ServiceAdaptor.NetCore.Client;
using ServiceAdaptor.NetCore;
using Microsoft.Extensions.DependencyModel;

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
                var assembly = LoadAssemblyByFile(assemblyFile);              

                //(x.3) create class
                return assembly?.CreateInstance(className) as IServiceAdaptor;
            }
            #endregion
        }





        #region LoadAssemblyByFile       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <returns></returns>
        public static Assembly LoadAssemblyByFile(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
            {
                return null;
            }

            Assembly assembly = null;

            #region (x.1) get assembly from dll file
            try
            {
                var filePath = CommonHelp.GetAbsPath(assemblyFile);
                if (File.Exists(filePath))
                {
                    assembly = Assembly.LoadFrom(filePath);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            #endregion


            var assemblyFileName = Path.GetFileNameWithoutExtension(assemblyFile);

            #region (x.2)Get from DependencyContext               
            if (assembly == null)
            {
                assembly = DependencyContext.Default.RuntimeLibraries
                 .Where(m => m.Name == assemblyFileName)
                 .Select(o => Assembly.Load(new AssemblyName(o.Name))).FirstOrDefault();
            }
            #endregion

            #region (x.3)Get from ReferencedAssemblies               
            if (assembly == null)
            {
                assembly = Assembly.GetEntryAssembly().GetReferencedAssemblies()
                    .Where(m => m.Name == assemblyFileName)
                    .Select(Assembly.Load).FirstOrDefault();
            }
            #endregion


            #region (x.4)Get from CurrentDomain
            if (assembly == null)
            {
                assembly = System.AppDomain.CurrentDomain.GetAssemblies().Where(asm => asm.ManifestModule.Name == assemblyFile).FirstOrDefault();
            }
            #endregion

            return assembly;
        }
        #endregion
    }
}
