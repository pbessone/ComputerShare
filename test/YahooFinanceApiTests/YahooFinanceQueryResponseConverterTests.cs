using Xunit;
using YahooFinanceApi;
using YahooFinanceApi.Converters;

namespace YahooFinanceApiTests
{
    public class YahooFinanceQueryResponseConverterTests
    {
        private readonly YahooFinanceQueryResponseConverter _yahooFinanceQueryResponseConverter;

        public YahooFinanceQueryResponseConverterTests()
        {
            _yahooFinanceQueryResponseConverter = new YahooFinanceQueryResponseConverter();            
        }
        
        [Fact]
        public void ConvertFrom_ApiResponseIsNull_ReturnNull()
        {
            // Arrange
            ApiResponse apiResponse = null;
            
            // Act
            var result = _yahooFinanceQueryResponseConverter.ConvertFrom(apiResponse);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void ConvertFrom_ValidApiResponse_ReturnConvertedResponse()
        {
            // Arrange
            ApiResponse apiResponse = new ApiResponse
            {
                Chart = new Chart
                {
                    Result = new []
                    {
                        new ChartResult
                        {
                            Meta = new ChartResultMeta
                            {
                                Symbol = "GOOG"
                            },
                            TimeStamp = new long[] { 1590586200 },
                            Indicators = new ChartResultIndicator
                            {
                                Quote = new []
                                {
                                    new ChartResultIndicatorQuote
                                    {
                                        Low = new decimal?[] { 1 },
                                        High = new decimal?[] { 1 },
                                        Open = new decimal?[] { 1 },
                                        Close = new decimal?[] { 1 }
                                    }
                                }
                            }
                        },
                    }
                }
            };
            
            // Act
            var result = _yahooFinanceQueryResponseConverter.ConvertFrom(apiResponse);
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(apiResponse.Chart.Result[0].Meta.Symbol, result.ShareName);
            Assert.Equal(apiResponse.Chart.Result[0].Indicators.Quote[0].Low, result.History.Low);
            Assert.Equal(apiResponse.Chart.Result[0].Indicators.Quote[0].High, result.History.High);
            Assert.Equal(apiResponse.Chart.Result[0].Indicators.Quote[0].Open, result.History.Open);
            Assert.Equal(apiResponse.Chart.Result[0].Indicators.Quote[0].Close, result.History.Close);
        }
    }
}