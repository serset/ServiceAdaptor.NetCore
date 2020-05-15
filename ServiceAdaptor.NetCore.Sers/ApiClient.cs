﻿#region << 版本注释-v4 >>
/*
 * ========================================================================
 * 版本：v4
 * 时间：2020-04-09
 * 说明： 
 * ========================================================================
*/
#endregion


using Sers.Core.Module.Message;
using Sers.Core.Module.Rpc;
using ServiceAdaptor.NetCore.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vit.Core.Util.Net;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.Sers
{
    public class ApiClient : IApiClient
    {         

        #region CallApi

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public ApiResponse<ReturnType> CallApi<ReturnType>(ApiRequest req)
        {
            #region (x.1)构建请求           
            var url = req.url;
            var arg = req.arg;
            if (arg != null && req.httpMethod == "GET")
            {
                url = HttpClient.UrlAddParams(url, arg);
                arg = null;
            }


            #region init rpc
            Action<IRpcContextData> InitRpc = null;
            if (req.headers == null)
            {
                req.headers = new Dictionary<string, string>();
            }
            req.headers.IDictionaryTryAdd("Content-Type", "application/json");            
            InitRpc =
                rpcData =>
                {                  
                    foreach (var item in req.headers)
                    {
                        rpcData.http_header_Set(item.Key, item.Value);
                    }
                };

            #endregion

            ApiMessage request = new ApiMessage().InitAsApiRequestMessage(url, arg, req.httpMethod, InitRpc);         

            #endregion

            //(x.2)发送请求
            ApiMessage response = global::Sers.Core.Module.Api.ApiClient.CallRemoteApi(request);

            #region (x.3)处理回应           
            var replyRpcData = RpcFactory.CreateRpcContextData();
            replyRpcData.UnpackOriData(response.rpcContextData_OriData);

            return new ApiResponse<ReturnType>
            {
                StatusCode = replyRpcData.http_statusCode_Get() ?? 0,
                data = response.value_OriData.DeserializeFromArraySegmentByte<ReturnType>(),
                headers = replyRpcData.http_headers_Get()?.ConvertBySerialize<IDictionary<string, string>>()
            };
            #endregion

        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResponse<ReturnType>> CallApiAsync<ReturnType>(ApiRequest req)
        {
            #region (x.1)构建请求           
            var url = req.url;
            var arg = req.arg;
            if (arg != null && req.httpMethod == "GET")
            {
                url = HttpClient.UrlAddParams(url, arg);
                arg = null;
            }

            #region init rpc
            Action<IRpcContextData> InitRpc = null;
            if (req.headers == null)
            {
                req.headers = new Dictionary<string, string>();
            }
            req.headers.IDictionaryTryAdd("Content-Type", "application/json");

            InitRpc =
                rpcData =>
                {                     
                    foreach (var item in req.headers)
                    {
                        rpcData.http_header_Set(item.Key, item.Value);
                    }
                };
            #endregion
            ApiMessage request = new ApiMessage().InitAsApiRequestMessage(url, arg, req.httpMethod, InitRpc);
            #endregion

            //(x.2)发送请求
            ApiMessage response = await global::Sers.Core.Module.Api.ApiClient.CallRemoteApiAsync(request);

            #region (x.3)处理回应           
            var replyRpcData = RpcFactory.CreateRpcContextData();
            replyRpcData.UnpackOriData(response.rpcContextData_OriData);

            return new ApiResponse<ReturnType>
            {
                StatusCode = replyRpcData.http_statusCode_Get() ?? 0,
                data = response.value_OriData.DeserializeFromArraySegmentByte<ReturnType>(),
                headers = replyRpcData.http_headers_Get()?.ConvertBySerialize<IDictionary<string, string>>()
            };
            #endregion

        }

        #endregion


    }
}
