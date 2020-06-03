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
            return new ShareHistoryQueryResponse
            {
                ShareName = result.Meta.Symbol,
                History = new ShareHistory
                {
                    TimeStamp = result.TimeStamp,
                    Open = quoteIndicators.Open,
                    Close = quoteIndicators.Close,
                    Low = quoteIndicators.Low,
                    High = quoteIndicators.High
                }
            };
        }
    }
}