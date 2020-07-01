#region << 版本注释-v1 >>
/*
 * ========================================================================
 * 版本：v1
 * 时间：2020-04-03
 * 说明： 
 * ========================================================================
*/
#endregion


using ServiceAdaptor.NetCore.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vit.Core.Util.Net;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.MinHttp
{
    public class ApiClient : IApiClient
    {

        public readonly HttpClient vitHttpClient = new HttpClient();

        public List<IApiEvent> apiEvents { get; set; }



        #region CallApi

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual ApiResponse<ReturnType> CallApi<ReturnType>(ApiRequest req) 
        {
            //(x.0)BeforeCallApi
            var apiResponse = this.BeforeCallApi<ReturnType>(req).Result;
            if (apiResponse != null) return apiResponse;


            var request = new HttpRequest { url = req.url, body = req.arg, httpMethod = req.httpMethod, headers = req.headers };

            if (request.body != null && request.httpMethod == "GET")
            {
                request.url = HttpClient.UrlAddParams(request.url, request.body);
                request.body = null;
            }

            var response = vitHttpClient.Send<ReturnType>(request);
            if (response == null)
            {
                return null;
            }
            return new ApiResponse<ReturnType> { StatusCode= response.StatusCode,data= response.data,headers= response.headers};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual async Task<ApiResponse<ReturnType>> CallApiAsync<ReturnType>(ApiRequest req)
        {
            //(x.0)BeforeCallApi
            var apiResponse = this.BeforeCallApi<ReturnType>(req).Result;
            if (apiResponse != null) return apiResponse;

            var request = new HttpRequest { url = req.url, body = req.arg, httpMethod = req.httpMethod, headers = req.headers };

            if (request.body != null && request.httpMethod == "GET")
            {
                request.url = HttpClient.UrlAddParams(request.url, request.body);
                request.body = null;
            }

            var response = await vitHttpClient.SendAsync<ReturnType>(request);
            if (response == null)
            {
                return null;
            }
            return new ApiResponse<ReturnType> { StatusCode = response.StatusCode, data = response.data, headers = response.headers };
        }

        #endregion



    }
}
