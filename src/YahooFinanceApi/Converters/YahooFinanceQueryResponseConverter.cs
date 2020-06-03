using ShareHistoryQueryApi;
using ShareHistoryQueryApi.Converters;

namespace YahooFinanceApi.Converters
{
    public class YahooFinanceQueryResponseConverter : IShareHistoryQueryResponseConverter<ApiResponse>
    {
        public ShareHistoryQueryResponse ConvertFrom(ApiResponse queryResponse)
        {
            if (queryResponse == null)
            {
                return null;
            }

            var result = queryResponse.Chart.Result[0];
            var quoteIndicators = result.Indicators.Quote[0];
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = result.Meta.Symbol,
                History = new ShareHistory()
            };

            // If timestamp is null it means there are no results for the period so we can simply ignore
            if (result.TimeStamp != null)
            {
                var history = shareHistoryQueryResponse.History;
                history.TimeStamp = result.TimeStamp;
                history.Open = quoteIndicators.Open;
                history.Close = quoteIndicators.Close;
                history.Low = quoteIndicators.Low;
                history.High = quoteIndicators.High;
            }

            return shareHistoryQueryResponse;
        }
    }
}