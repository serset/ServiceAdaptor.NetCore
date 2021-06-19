

using Vit.Core.Util.ComponentModel.Data;
using System.Threading.Tasks;
using Service.ServiceProvider.Contract;
using ServiceAdaptor.NetCore.Client;
using Vit.Extensions;

namespace Service.ServiceProvider
{
    public class ServiceProviderService
    {


        #region Get

        public static async Task<ApiReturn<UserInfo>> GetApiReturnAsync(UserInfo userInfo)
        {

            //var apiRequest = new ApiRequest { url = "/ServiceProvider/Values/item", arg = userInfo, httpMethod = ApiClient.Get };
            //apiRequest.SetServiceName("ServiceProvider");

            return await ApiClient.CallApiAsync<ApiReturn<UserInfo>>("/ServiceProvider/Values/item", userInfo, ApiClient.Get);
        }


        public static async Task<UserInfo> GetItemAsync(UserInfo userInfo)
        {
            return await ApiClient.CallVitApiAsync<UserInfo>("/ServiceProvider/Values/item", userInfo, ApiClient.Get);
        }


        public static ApiReturn<UserInfo> GetApiReturn(UserInfo userInfo)
        {
            return ApiClient.CallApi<ApiReturn<UserInfo>>("/ServiceProvider/Values/item", userInfo, ApiClient.Get);
        }

        public static UserInfo GetItem(UserInfo userInfo)
        {
            return ApiClient.CallVitApi<UserInfo>("/ServiceProvider/Values/item", userInfo, ApiClient.Get);
        }

        #endregion



        #region Post

        public static async Task<ApiReturn<UserInfo>> PostApiReturnAsync(UserInfo userInfo)
        {
            return await ApiClient.CallApiAsync<ApiReturn<UserInfo>>("/ServiceProvider/Values/item", userInfo, ApiClient.Post);
        }
       
        #endregion


    }
}
