using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ServiceAdaptor.NetCore.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Vit.Core.Module.Log;
using Vit.Extensions;
using Vit.WebHost;

namespace ServiceAdaptor.NetCore.Gateway
{
    public class GatewayHost
    {

        static string prefixOfCopyIpToHeader = Vit.Core.Util.ConfigurationManager.ConfigurationManager.Instance.GetStringByPath("Gateway.prefixOfCopyIpToHeader");

        static string ResponseDefaultContentType = (Vit.Core.Util.ConfigurationManager.ConfigurationManager.Instance.GetStringByPath("Gateway.ResponseDefaultContentType") ?? ("application/json; charset=" + Vit.Core.Module.Serialization.Serialization_Newtonsoft.Instance.charset));
        static async Task Bridge(HttpContext context)
        {
            try
            {
                #region (x.1)build apiRequest

                var request = context.Request;

                var url = request.PathBase + request.Path.Value + request.QueryString.Value;


                #region (x.x.2)headers
                //(x.x.x.1)
                var headers = new Dictionary<string, string>();
                foreach (var kv in request.Headers)
                {
                    headers[kv.Key] = kv.Value.ToString();
                }

                //(x.x.x.2)记录Ip 到 headers
                if (prefixOfCopyIpToHeader != null)
                {
                    headers[prefixOfCopyIpToHeader + "RemoteIpAddress"] = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    headers[prefixOfCopyIpToHeader + "RemotePort"] = context.Connection.RemotePort.ToString();

                    headers[prefixOfCopyIpToHeader + "LocalIpAddress"] = context.Connection.LocalIpAddress.MapToIPv4().ToString();
                    headers[prefixOfCopyIpToHeader + "LocalPort"] = context.Connection.LocalPort.ToString();
                }
                #endregion


                #region (x.x.3)body
                byte[] arg = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    //request.Body.CopyTo(ms);
                    await request.Body.CopyToAsync(ms);
                    if (ms.Length > 0)
                    {
                        arg = ms.ToArray();
                    }
                }
                #endregion

                //(x.x.4)build apiRequest
                var apiRequest = new ApiRequest { url = url, arg = arg, httpMethod = request.Method, headers = headers };

                #endregion

                #region (x.2)CallApi             
               
                var apiResponse = await ApiClient.Instance.CallApiAsync<byte[]>(apiRequest);
                #endregion

                #region (x.3)Write ApiResponse

                var response = context.Response;

                //(x.x.1)StatusCode
                response.StatusCode = apiResponse.StatusCode;

                #region (x.x.2) header
                {
                    //(x.x.x.1)原始header
                    var responseHeaders = response.Headers;
                    if (null != apiResponse.headers)
                    {
                        foreach (var item in apiResponse.headers)
                        {
                            responseHeaders.TryAdd(item.Key, item.Value);
                        }
                    }


                    //(x.x.x.2)Content-Type → application/json
                    if (!headers.ContainsKey("Content-Type"))
                    {
                        headers["Content-Type"] = ResponseDefaultContentType;
                        //response.ContentType = "application/json";
                    }
                }
                #endregion

                //(x.x.3) Body             
                if (apiResponse.data != null && apiResponse.data.Length > 0)
                {                  
                    await response.Body.WriteAsync(apiResponse.data);
                }
                #endregion                 
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }



        public static void StartWebHost()
        {
            try
            {

                HostRunArg arg = Vit.Core.Util.ConfigurationManager.ConfigurationManager.Instance.GetByPath<HostRunArg>("Gateway");
                if (arg == null || arg.urls == null || arg.urls.Length == 0) return;


                #region (x.3)初始化WebHost

                //(x.x.1)指定可以与iis集成（默认无法与iis集成）
                arg.OnCreateWebHostBuilder = () => Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseVitConfig();


                #region (x.x.2)转发web请求调用(网关核心功能)
                arg.OnConfigure = (app) =>
                {
                    app.Run(Bridge);
                };
                #endregion


                //(x.x.3)设置非异步启动
                arg.RunAsync = true;


                #region (x.x.4)启动           
                Logger.Info("[WebHost]listening", arg.urls);

                if (arg.staticFiles?.rootPath != null)
                    Logger.Info("[WebHost]wwwroot path", arg.staticFiles?.rootPath);

                Vit.WebHost.Host.Run(arg);
                #endregion


                #endregion

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Console.WriteLine("Exception:" + ex.Message);
            }


        }
    }
}
