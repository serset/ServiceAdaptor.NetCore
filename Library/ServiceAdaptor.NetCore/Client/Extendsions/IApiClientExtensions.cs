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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vit.Core.Util.ComponentModel.Data;

namespace Vit.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class IApiClientExtensions
    {

        #region Event

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Instance"></param>
        /// <param name="apiEvent"></param>
        public static void AddEvent(this IApiClient Instance, IApiEvent apiEvent)
        {
            if (Instance.apiEvents == null) Instance.apiEvents = new System.Collections.Generic.List<IApiEvent>();
            Instance.apiEvents.Add(apiEvent);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Instance"></param>
        /// <param name="apiEvent"></param>
        public static void RemoveEvent(this IApiClient Instance, IApiEvent apiEvent)
        {
            Instance.apiEvents?.Remove(apiEvent);
        }


        public static async Task<ApiResponse<ReturnType>> BeforeCallApi<ReturnType>(this IApiClient apiClient, ApiRequest req)
        {
            if (apiClient.apiEvents != null)
            {
                foreach (var apiEvent in apiClient.apiEvents)
                {
                    var apiResponse = await apiEvent.BeforeCallApi<ReturnType>(req);
                    if (apiResponse != null) return apiResponse;
                }
            }
            return null;
        }
        #endregion




        #region CallApi

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Instance"></param>
        /// <param name="url"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <param name="headers">请求的header,可不指定</param>
        /// <returns></returns>
        public static ReturnType CallApi<ReturnType>(this IApiClient Instance, string url, Object arg,
            string httpMethod = null, IDictionary<string, string> headers = null)
        {
            var response = Instance.CallApi<ReturnType>(new ApiRequest { url = url, arg = arg, httpMethod = httpMethod, headers = headers });
            if (response == null)
            {
                return default;
            }
            return response.data;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="Instance"></param>
        /// <param name="url"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <param name="headers">请求的header,可不指定</param>
        /// <returns></returns>
        public static async Task<ReturnType> CallApiAsync<ReturnType>(this IApiClient Instance, string url, Object arg,
            string httpMethod = null, IDictionary<string, string> headers = null)
        {
            var response = await Instance.CallApiAsync<ReturnType>(new ApiRequest { url = url, arg = arg, httpMethod = httpMethod, headers = headers });
            if (response == null)
            {
                return default;
            }
            return response.data;
        }

        #endregion



        #region CallVitApi by ApiRequest

        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary> 
        /// <param name="Instance"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static void CallVitApi(this IApiClient Instance, ApiRequest arg)
        {
            var apiRet = Instance.CallApi<ApiReturn>(arg)?.data;
            if (apiRet?.success != true)
            {
                throw (apiRet?.error).ToException("服务出错");
            }   
        }


        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <param name="Instance"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static async Task CallVitApiAsync(this IApiClient Instance, ApiRequest arg)
        {
            var apiRet = (await Instance.CallApiAsync<ApiReturn>(arg))?.data;
            if (apiRet?.success != true)
            {
                throw (apiRet?.error).ToException("服务出错");
            }      
        }



        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="Instance"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static ReturnType CallVitApi<ReturnType>(this IApiClient Instance, ApiRequest arg)
        {
            var apiRet = Instance.CallApi<ApiReturn<ReturnType>>(arg)?.data;
            if (apiRet?.success != true)
            {
                throw (apiRet?.error).ToException("服务出错");
            }
            return apiRet.data;
        }


        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="Instance"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static async Task<ReturnType> CallVitApiAsync<ReturnType>(this IApiClient Instance, ApiRequest arg)
        {
            var apiRet = (await Instance.CallApiAsync<ApiReturn<ReturnType>>(arg))?.data;
            if (apiRet?.success != true)
            {
                throw (apiRet?.error).ToException("服务出错");
            }
            return apiRet.data;
        }


        #endregion

        #region CallVitApi by route arg httpMethod headers

        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <param name="Instance"></param>
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <param name="headers">请求的header,可不指定</param>
        /// <returns></returns>
        public static void CallVitApi(this IApiClient Instance, string route, Object arg,
            string httpMethod = null, IDictionary<string, string> headers = null)
        {
            CallVitApi(Instance, new ApiRequest { url = route, arg = arg, httpMethod = httpMethod, headers = headers });
        }


        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <param name="Instance"></param>
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <param name="headers">请求的header,可不指定</param>
        /// <returns></returns>
        public static async Task CallVitApiAsync(this IApiClient Instance, string route, Object arg,
            string httpMethod = null, IDictionary<string, string> headers = null)
        {
            await CallVitApiAsync(Instance, new ApiRequest { url = route, arg = arg, httpMethod = httpMethod, headers = headers });
        }




        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="Instance"></param>
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <param name="headers">请求的header,可不指定</param>
        /// <returns></returns>
        public static ReturnType CallVitApi<ReturnType>(this IApiClient Instance, string route, Object arg,
            string httpMethod = null, IDictionary<string, string> headers = null)
        {
            return CallVitApi<ReturnType>(Instance, new ApiRequest { url = route, arg = arg, httpMethod = httpMethod, headers = headers });
        }


        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="Instance"></param>
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <param name="headers">请求的header,可不指定</param>
        /// <returns></returns>
        public static async Task<ReturnType> CallVitApiAsync<ReturnType>(this IApiClient Instance, string route, Object arg,
            string httpMethod = null, IDictionary<string, string> headers = null)
        {
            return await CallVitApiAsync<ReturnType>(Instance, new ApiRequest { url = route, arg = arg, httpMethod = httpMethod, headers = headers });
        }

        #endregion

    }
}
