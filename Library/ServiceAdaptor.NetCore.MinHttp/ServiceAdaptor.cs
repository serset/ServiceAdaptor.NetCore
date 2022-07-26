using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using ServiceAdaptor.NetCore.Client;
using Vit.Core.Module.Log;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.MinHttp
{
    public class ServiceAdaptor : IServiceAdaptor
    {
        public IWebHostBuilder InitWebHostBuilder(IWebHostBuilder builder, JObject config, out IApiClient apiClient)
        {       

            #region (x.)构建ApiClient     

            var httpApi = new ApiClient();
            apiClient = httpApi;


            #region (x.x.1)gatewayAddress
            var gatewayAddress = config["gatewayAddress"].ConvertToString();
            httpApi.vitHttpClient.BaseAddress = gatewayAddress;
            #endregion


            #region (x.x.2)超时时间
            var timeoutSeconds = config["timeoutSeconds"].ConvertBySerialize<int?>(); 
            if (timeoutSeconds.HasValue)
            {
                httpApi.vitHttpClient.TimeoutSeconds = timeoutSeconds.Value;
            }
            #endregion

            #endregion

            Logger.Info("[ServiceAdaptor.NetCore.MinHttp]配置", new { gatewayAddress });


            return builder;
        }
    }
}
