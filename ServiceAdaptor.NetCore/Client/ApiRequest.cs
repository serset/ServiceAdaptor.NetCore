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
    public class ApiRequest:Vit.Core.Util.Extensible.Extensible
    {
        /// <summary>
        /// 为相对路径，如 "/api/Values/get"
        /// </summary>
        public string url;
        /// <summary>
        /// 
        /// </summary>
        public Object arg;
        /// <summary>
        /// 可为 GET、POST、DELETE、PUT等,可不指定
        /// </summary>
        public string httpMethod;
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> headers;
    }
}
