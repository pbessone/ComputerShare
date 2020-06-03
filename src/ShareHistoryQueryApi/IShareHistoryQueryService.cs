using System.Threading.Tasks;

namespace ShareHistoryQueryApi
{
    public interface IShareHistoryQueryService
    {
        Task<ShareHistoryQueryResponse> Query(string symbol, ShareHistoryQueryRange range);
    }
}