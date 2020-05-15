using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using ServiceAdaptor.NetCore.Client;

namespace ServiceAdaptor.NetCore
{
    public interface IServiceAdaptor
    {
        IWebHostBuilder InitWebHostBuilder(IWebHostBuilder builder, JObject config,out IApiClient apiClient);
    }
}
