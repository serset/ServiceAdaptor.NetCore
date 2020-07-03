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

namespace ServiceAdaptor.NetCore.Client
{

    public interface IApiClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        ApiResponse<ReturnType> CallApi<ReturnType>(ApiRequest req);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<ReturnType>> CallApiAsync<ReturnType>(ApiRequest req);


        List<IApiEvent> apiEvents{ get; set; }

    }
}
