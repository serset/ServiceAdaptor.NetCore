#region << 版本注释-v1 >>
/*
 * ========================================================================
 * 版本：v1
 * 时间：2020-04-03
 * 说明： 
 * ========================================================================
*/
#endregion


using System.Threading.Tasks;

namespace ServiceAdaptor.NetCore.Client
{
    public interface IApiEvent
    {
        Task<ApiResponse<ReturnType>> BeforeCallApi<ReturnType>(ApiRequest req);
    }
}
