#region << 版本注释-v1 >>
/*
 * ========================================================================
 * 版本：v1
 * 时间：2020-04-03
 * 说明： 
 * ========================================================================
*/
#endregion


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.Client
{

    public class ApiClient
    {

        public static IApiClient Instance { get; internal set; } 
        

        #region CallApi

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<ReturnType> CallApiAsync<ReturnType>(string route, Object arg, string httpMethod = null, IDictionary<string, string> headers=null)
        {
            var response = await Instance.CallApiAsync<ReturnType>(new ApiRequest { url = route, arg = arg, httpMethod = httpMethod, headers= headers });
            if (response == null)
            {
                return default;
            }
            return response.data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <returns></returns>
        public static ReturnType CallApi<ReturnType>(string route, Object arg, string httpMethod = null)
        {
            var response = Instance.CallApi<ReturnType>(new ApiRequest { url = route, arg = arg, httpMethod = httpMethod });
            if (response == null) 
            {
                return default;
            }
            return response.data;
        }

        #endregion


        #region CallVitApi

        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <returns></returns>
        public static void CallVitApi(string route, Object arg, string httpMethod = null)
        {
            Instance.CallVitApi(route, arg, httpMethod);
        }


        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>     
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <returns></returns>
        public static async Task CallVitApiAsync(string route, Object arg, string httpMethod = null)
        {
            await Instance.CallVitApiAsync(route, arg, httpMethod);
        }



        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <returns></returns>
        public static ReturnType CallVitApi<ReturnType>(string route, Object arg, string httpMethod = null)
        {
            return Instance.CallVitApi<ReturnType>(route, arg, httpMethod);
        }


        /// <summary>
        /// 接口返回数据为 ApiReturn格式，若接口返回不成功（apiRet?.success != true），则直接抛异常。
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="route"></param>
        /// <param name="arg"></param>
        /// <param name="httpMethod">可为 GET、POST、DELETE、PUT等,可不指定</param>
        /// <returns></returns>
        public static async Task<ReturnType> CallVitApiAsync<ReturnType>(string route, Object arg, string httpMethod = null)
        {
            return await Instance.CallVitApiAsync<ReturnType>(route, arg, httpMethod);
        }

        #endregion




        #region httpMethod
        public const string Get = "GET";
        public const string Post = "POST";
        public const string Delete = "DELETE";
        public const string Put = "PUT";
        public const string Patch = "PATCH";
        #endregion




    }
}
