using System.Threading.Tasks;

namespace ShareHistoryQueyApi
{
    public interface IShareHistoryQueryService
    {
        Task<ShareHistoryQueryResponse> Query(string symbol, ShareHistoryQueryRange range);
    }
}