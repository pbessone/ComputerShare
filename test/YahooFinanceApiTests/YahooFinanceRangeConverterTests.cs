using System;
using ShareHistoryQueryApi;
using Xunit;
using YahooFinanceApi.Converters;

namespace YahooFinanceApiTests
{
    public class YahooFinanceRangeConverterTests
    {
        private readonly YahooFinanceRangeConverter _yahooFinanceRangeConverter;

        public YahooFinanceRangeConverterTests()
        {
            _yahooFinanceRangeConverter = new YahooFinanceRangeConverter();            
        }
        
        [Fact]
        public void ConvertFrom_OneDayQueryRange_ReturnYahooEquivalentRange()
        {
            // Arrange
            var range = ShareHistoryQueryRange.OneDay;
            
            // Act
            var yahooRange = _yahooFinanceRangeConverter.ConvertFrom(range);
            
            // Assert
            Assert.Equal("1d", yahooRange);
        }
        
        [Fact]
        public void ConvertFrom_OneWeekQueryRange_ReturnYahooEquivalentRange()
        {
            // Arrange
            var range = ShareHistoryQueryRange.OneWeek;
            
            // Act
            var yahooRange = _yahooFinanceRangeConverter.ConvertFrom(range);
            
            // Assert
            Assert.Equal("5d", yahooRange);
        }
        
        [Fact]
        public void ConvertFrom_OneMonthQueryRange_ReturnYahooEquivalentRange()
        {
            // Arrange
            var range = ShareHistoryQueryRange.OneMonth;
            
            // Act
            var yahooRange = _yahooFinanceRangeConverter.ConvertFrom(range);
            
            // Assert
            Assert.Equal("1mo", yahooRange);
        }
        
        [Fact]
        public void ConvertFrom_ThreeMonthsQueryRange_ReturnYahooEquivalentRange()
        {
            // Arrange
            var range = ShareHistoryQueryRange.ThreeMonths;
            
            // Act
            var yahooRange = _yahooFinanceRangeConverter.ConvertFrom(range);
            
            // Assert
            Assert.Equal("3mo", yahooRange);
        }
        
        [Fact]
        public void ConvertFrom_UnsupportedRange_ThrowsNotSupportedException()
        {
            // Arrange
            var range = ShareHistoryQueryRange.SixMonths;
            
            // Act/Assert
            Assert.Throws<NotSupportedException>(() => _yahooFinanceRangeConverter.ConvertFrom(range));
        }
    }
}