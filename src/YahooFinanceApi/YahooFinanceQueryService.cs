using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ShareHistoryQueryApi;
using ShareHistoryQueryApi.Converters;
using ShareHistoryQueryApi.Exceptions;

namespace YahooFinanceApi
{
    public class YahooFinanceQueryService : IShareHistoryQueryService
    {
        private readonly string _baseUrl = "https://query1.finance.yahoo.com/v8/finance/chart";
        private readonly HttpClient _httpClient;
        private readonly IShareHistoryQueryResponseConverter<ApiResponse> _shareHistoryQueryResponseConverter;
        private readonly IShareHistoryQueryRangeConverter _shareHistoryQueryRangeConverter;

        public YahooFinanceQueryService(
            HttpClient httpClient,
            IShareHistoryQueryResponseConverter<ApiResponse> shareHistoryQueryResponseConverter,
            IShareHistoryQueryRangeConverter shareHistoryQueryRangeConverter)
        {
            _httpClient = httpClient;
            _shareHistoryQueryResponseConverter = shareHistoryQueryResponseConverter;
            _shareHistoryQueryRangeConverter = shareHistoryQueryRangeConverter;
        }
        
        public async Task<ShareHistoryQueryResponse> Query(string symbol, ShareHistoryQueryRange range)
        {
            var yahooRange = _shareHistoryQueryRangeConverter.ConvertFrom(range);
            var interval = YahooInterval.OneDay; // This could be reduced up to 1 minute if we want more granular and precise data
            var url = $"{_baseUrl}/{symbol}?range={yahooRange}&interval={interval}";
            using var response = await _httpClient.GetAsync(url);

            var result = await ParseResponse(response);
            
            if (response.IsSuccessStatusCode)
            {
                return _shareHistoryQueryResponseConverter.ConvertFrom(result);
            }
            
            var error = result.Chart.Error;
            throw new InvalidRequestException(error.Code, error.Description);
        }
        
        private static async Task<ApiResponse> ParseResponse(HttpResponseMessage response)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse>();
            }
            catch (JsonException)
            {
                throw new InvalidRequestException("JSON Exception", "There has been a problem parsing the response from the service");
            }
        }
    }
}