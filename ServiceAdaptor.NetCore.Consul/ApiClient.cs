using Consul;
using ServiceAdaptor.NetCore.Client;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.Consul
{
    public class ApiClient : global::ServiceAdaptor.NetCore.MinHttp.ApiClient
    {
        public ApiClient(string ConsulAddress) 
        {
            this.ConsulAddress = ConsulAddress;
        }


        /// <summary>
        ///  http://127.0.0.1:8500
        /// </summary>
        string ConsulAddress;


        #region FindServiceByServiceName

        CatalogService FindServiceByServiceName(string serviceName) 
        {
            using (var consulClient = new ConsulClient(a => a.Address = new Uri(ConsulAddress)))
            {         
                var services = consulClient.Catalog.Service(serviceName).Result.Response;
                if (services != null && services.Any())
                {
                    // 模拟随机一台进行请求，这里只是测试，可以选择合适的负载均衡工具或框架
                    Random r = new Random();
                    int index = r.Next(services.Count());
                    var service = services.ElementAt(index);
                    return service;
                    //using (HttpClient client = new HttpClient())
                    //{
                    //    var response = await client.GetAsync($"http://{service.ServiceAddress}:{service.ServicePort}/values/test");
                    //    var result = await response.Content.ReadAsStringAsync();
                    //    return result;
                    //}
                }
                return null;
            } 
        }
        #endregion



        protected async Task<ApiResponse<ReturnType>> BeforeCallApi<ReturnType>(ApiRequest req)
        {           
            var serviceInfo= FindServiceByServiceName( req.GetServiceName());

            if (serviceInfo == null) 
            {
                return new ApiResponse<ReturnType> { StatusCode = (int)HttpStatusCode.NotFound }; 
            }

            req.url = "http://" + serviceInfo.ServiceAddress + ":" + serviceInfo.ServicePort +  req.url;

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
            var apiResponse=BeforeCallApi<ReturnType>(req).Result;         

            return apiResponse ?? base.CallApi<ReturnType>(req);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public override  async Task<ApiResponse<ReturnType>> CallApiAsync<ReturnType>(ApiRequest req)
        {
            var apiResponse =  await BeforeCallApi<ReturnType>(req);

            return apiResponse ??  await base.CallApiAsync<ReturnType>(req);
        }

        #endregion


    }
}
