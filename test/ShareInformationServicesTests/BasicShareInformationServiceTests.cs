using System;
using System.Threading.Tasks;
using Moq;
using ShareHistoryQueryApi;
using ShareInformationServices;
using Xunit;

namespace ShareInformationServicesTests
{
    public class BasicShareInformationServiceTests
    {
        private readonly Mock<IShareHistoryQueryService> _mockShareHistoryQueryService;
        private readonly BasicShareInformationService _basicShareInformationService;

        public BasicShareInformationServiceTests()
        {
            _mockShareHistoryQueryService = new Mock<IShareHistoryQueryService>();
            _basicShareInformationService = new BasicShareInformationService(_mockShareHistoryQueryService.Object);            
        }
        
        [Fact]
        public async Task GetBasicShareInformation_SymbolIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            string symbol = null;
            
            // Act/Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _basicShareInformationService.GetBasicShareInformationAsync(symbol));
        }

        [Fact]
        public async Task GetBasicShareInformation_SymbolIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            string symbol = string.Empty;
            
            // Act/Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _basicShareInformationService.GetBasicShareInformationAsync(symbol));
        }
        
        [Fact]
        public async Task GetBasicShareInformation_SymbolIsWhitespace_ThrowsArgumentNullException()
        {
            // Arrange
            string symbol = "    ";
            
            // Act/Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _basicShareInformationService.GetBasicShareInformationAsync(symbol));
        }
        
        [Fact]
        public async Task GetBasicShareInformation_ResponseHasNoHistory_MinimumSharePriceForPeriodIsZero()
        {
            // Arrange
            string symbol = "GOOG";
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = symbol,
                History = new ShareHistory()
            };
            _mockShareHistoryQueryService
                .Setup(x => x.Query(symbol, It.IsAny<ShareHistoryQueryRange>()))
                .ReturnsAsync(shareHistoryQueryResponse);
                
            // Act
            var basicShareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            
            //Assert
            Assert.Equal(0, basicShareInformation.MinimumSharePriceForPeriod);
        }

        [Fact]
        public async Task GetBasicShareInformation_HistoryHasLowIndicatorsWithNullValues_MinimumSharePriceForPeriodIsSmallestNotNullLowIndicator()
        {
            // Arrange
            string symbol = "GOOG";
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = symbol,
                History = new ShareHistory
                {
                    Low = new decimal?[] { 1, null, 3 }
                }
            };
            _mockShareHistoryQueryService
                .Setup(x => x.Query(symbol, It.IsAny<ShareHistoryQueryRange>()))
                .ReturnsAsync(shareHistoryQueryResponse);
                
            // Act
            var basicShareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            
            //Assert
            Assert.Equal(1, basicShareInformation.MinimumSharePriceForPeriod);
        }
        
        [Fact]
        public async Task GetBasicShareInformation_HistoryHasLowIndicators_MinimumSharePriceForPeriodIsSmallestLowIndicator()
        {
            // Arrange
            string symbol = "GOOG";
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = symbol,
                History = new ShareHistory
                {
                    Low = new decimal?[] { 1, 2, 3 }
                }
            };
            _mockShareHistoryQueryService
                .Setup(x => x.Query(symbol, It.IsAny<ShareHistoryQueryRange>()))
                .ReturnsAsync(shareHistoryQueryResponse);
                
            // Act
            var basicShareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            
            //Assert
            Assert.Equal(1, basicShareInformation.MinimumSharePriceForPeriod);
        }
        
        [Fact]
        public async Task GetBasicShareInformation_ResponseHasNoHistory_MaximumSharePriceForPeriodIsZero()
        {
            // Arrange
            string symbol = "GOOG";
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = symbol,
                History = new ShareHistory()
            };
            _mockShareHistoryQueryService
                .Setup(x => x.Query(symbol, It.IsAny<ShareHistoryQueryRange>()))
                .ReturnsAsync(shareHistoryQueryResponse);
                
            // Act
            var basicShareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            
            //Assert
            Assert.Equal(0, basicShareInformation.MaximumSharePriceForPeriod);
        }

        [Fact]
        public async Task GetBasicShareInformation_HistoryHasHighIndicatorsWithNullValues_MaximumSharePriceForPeriodIsLargestNotNullHighIndicator()
        {
            // Arrange
            string symbol = "GOOG";
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = symbol,
                History = new ShareHistory
                {
                    High = new decimal?[] { 2, null, 3 }
                }
            };
            _mockShareHistoryQueryService
                .Setup(x => x.Query(symbol, It.IsAny<ShareHistoryQueryRange>()))
                .ReturnsAsync(shareHistoryQueryResponse);
                
            // Act
            var basicShareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            
            //Assert
            Assert.Equal(3, basicShareInformation.MaximumSharePriceForPeriod);
        }
        
        [Fact]
        public async Task GetBasicShareInformation_HistoryHasHighIndicators_MaximumSharePriceForPeriodIsLargestHighIndicator()
        {
            // Arrange
            string symbol = "GOOG";
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = symbol,
                History = new ShareHistory
                {
                    High = new decimal?[] { 1, 2, 3 }
                }
            };
            _mockShareHistoryQueryService
                .Setup(x => x.Query(symbol, It.IsAny<ShareHistoryQueryRange>()))
                .ReturnsAsync(shareHistoryQueryResponse);
                
            // Act
            var basicShareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            
            //Assert
            Assert.Equal(3, basicShareInformation.MaximumSharePriceForPeriod);
        }
        
        [Fact]
        public async Task GetBasicShareInformation_ResponseHasNoHistory_AverageSharePriceForPeriodIsZero()
        {
            // Arrange
            string symbol = "GOOG";
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = symbol,
                History = new ShareHistory()
            };
            _mockShareHistoryQueryService
                .Setup(x => x.Query(symbol, It.IsAny<ShareHistoryQueryRange>()))
                .ReturnsAsync(shareHistoryQueryResponse);
                
            // Act
            var basicShareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            
            //Assert
            Assert.Equal(0, basicShareInformation.AverageSharePriceForPeriod);
        }
        
        [Fact]
        public async Task GetBasicShareInformation_HistoryHasIndicatorsWithNullValues_NullValuesAreIgnored()
        {
            // Arrange
            string symbol = "GOOG";
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = symbol,
                History = new ShareHistory
                {
                    TimeStamp = new long[] { 1590586200, 1590672600, 1590759000 },
                    Low = new decimal?[] { 1, null, 1 },
                    High = new decimal?[] { null, null, null },
                    Open = new decimal?[] { null, null, 1 },
                    Close = new decimal?[] { 3, 3, 3 }
                }
            };
            _mockShareHistoryQueryService
                .Setup(x => x.Query(symbol, It.IsAny<ShareHistoryQueryRange>()))
                .ReturnsAsync(shareHistoryQueryResponse);
                
            // Act
            var basicShareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            
            //Assert
            Assert.Equal(2, basicShareInformation.AverageSharePriceForPeriod);
        }
        
        [Fact]
        public async Task GetBasicShareInformation_HistoryHasAllIndicatorsPopulated_AveragePriceIsCalculatedForAllValues()
        {
            // Arrange
            string symbol = "GOOG";
            var shareHistoryQueryResponse = new ShareHistoryQueryResponse
            {
                ShareName = symbol,
                History = new ShareHistory
                {
                    TimeStamp = new long[] { 1590586200, 1590672600, 1590759000 },
                    Low = new decimal?[] { 1, 1, 1 },
                    High = new decimal?[] { 2, 2, 2 },
                    Open = new decimal?[] { 3, 3, 3 },
                    Close = new decimal?[] { 4, 4, 4 }
                }
            };
            _mockShareHistoryQueryService
                .Setup(x => x.Query(symbol, It.IsAny<ShareHistoryQueryRange>()))
                .ReturnsAsync(shareHistoryQueryResponse);
                
            // Act
            var basicShareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            
            //Assert
            Assert.Equal(2.5m, basicShareInformation.AverageSharePriceForPeriod);
        }
    }
}