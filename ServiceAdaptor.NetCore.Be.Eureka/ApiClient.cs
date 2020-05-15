﻿
using RestTemplateCore;
using ServiceAdaptor.NetCore.Client;
using Steeltoe.Discovery.Eureka.Transport;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.Be.Eureka
{
    public class ApiClient : global::ServiceAdaptor.NetCore.MinHttp.ApiClient
    {
        EurekaHttpClient client => Blueearth.Common.MyHttpContext.EurekaClient;

     

        protected async Task<ApiResponse<ReturnType>> BeforeCallApi<ReturnType>(ApiRequest req)
        {
            #region header.Authorization
            {
                var Authorization = BlueearthHttpContext.Current?.Request?.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(Authorization))
                {
                    var header = req.headers;
                    if (header == null)
                    {
                        header = new Dictionary<string, string>();
                    }
                    header.TryAdd("Authorization", Authorization);
                }
            }
            #endregion


            #region redirect url
            {
                //"http://client-test-service/client/Enterprise_project/GetCompaniesby"
                var beEurekaUrl = "http://"+ req.GetServiceName()+ req.url;

                var rest = new RestTemplate(client, vitHttpClient.httpClient);
                //var service = await rest.ResolveRootUrlAsync("file-service");
                try
                {
                    req.url = await rest.ResolveUrlAsync(beEurekaUrl);
                }
                catch (System.Exception)
                {
                    return new ApiResponse<ReturnType> { StatusCode = (int)HttpStatusCode.NotFound };
                }
            }
            #endregion

            return null;
        }


        #region CallApi

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public override ApiResponse<ReturnType> CallApi<ReturnType>(ApiRequest req)
        {
            var apiResponse = BeforeCallApi<ReturnType>(req).Result;

            return apiResponse ?? base.CallApi<ReturnType>(req); 
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public override async Task<ApiResponse<ReturnType>> CallApiAsync<ReturnType>(ApiRequest req)
        {
            var apiResponse = await BeforeCallApi<ReturnType>(req);

            return apiResponse ?? await base.CallApiAsync<ReturnType>(req);
        }

        #endregion


    }
}
