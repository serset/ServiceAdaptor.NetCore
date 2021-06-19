using ServiceAdaptor.NetCore.Client;

namespace Vit.Extensions
{
    public static class ApiRequestExtensions
    {
        
        public static string GetServiceName(this ApiRequest data)
        {
            var serviceName= data.GetData<string>("serviceName");

            if (string.IsNullOrEmpty(serviceName)) 
            {
                var apiStation = data.url.Split('/')[1];
                return  apiStation; 
            }
            return serviceName;
        }

        public static void SetServiceName(this ApiRequest data,string serviceName)
        {
            data.SetData("serviceName", serviceName);
        }
    }
}
