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

namespace ServiceAdaptor.NetCore.Client
{
    public class ApiResponse<T> : Vit.Core.Util.Extensible.Extensible
    {
        public T data;

        public int StatusCode = 200;

        public IDictionary<string, string> headers;

        /// <summary>
        /// 返回结果:
        ///     A value that indicates if the HTTP response was successful. true if System.Net.Http.HttpResponseMessage.StatusCode
        ///     was in the range 200-299; otherwise false.
        /// </summary>
        public bool IsSuccessStatusCode => StatusCode >= 200 && StatusCode <= 299;
    }
}
