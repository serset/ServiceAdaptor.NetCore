using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using ServiceAdaptor.NetCore.Client;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.Sers
{
    public class ServiceAdaptor : IServiceAdaptor
    {
        public IWebHostBuilder InitWebHostBuilder(IWebHostBuilder builder, JObject config, out IApiClient apiClient)
        {
            apiClient = new ApiClient();

            builder.UseSerslot();

            return builder;
        }
    }
}
