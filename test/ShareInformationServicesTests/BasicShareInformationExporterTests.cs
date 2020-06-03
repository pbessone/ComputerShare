using System.Threading.Tasks;
using Moq;
using ShareInformationServices;
using Xunit;

namespace ShareInformationServicesTests
{
    public class BasicShareInformationExporterTests
    {
        private readonly Mock<IBasicShareInformationService> _mockBasicShareInformationService;
        private readonly BasicShareInformationExporter _basicShareInformationExporter;

        public BasicShareInformationExporterTests()
        {
            _mockBasicShareInformationService = new Mock<IBasicShareInformationService>();
            _basicShareInformationExporter = new BasicShareInformationExporter(_mockBasicShareInformationService.Object);            
        }
        
        [Fact]
        public async Task GetAsJsonString_CanFetchBasicShareInformation_ReturnJsonString()
        {
            // Arrange
            string symbol = "GOOG";
            var basicShareInformation = new BasicShareInformation
            {
                ShareName = symbol,
                MinimumSharePriceForPeriod = 1,
                MaximumSharePriceForPeriod = 3,
                AverageSharePriceForPeriod = 2
            };
            
            _mockBasicShareInformationService
                .Setup(x => x.GetBasicShareInformationAsync(symbol))
                .ReturnsAsync(basicShareInformation);
                
            // Act
            var jsonString = await _basicShareInformationExporter.GetAsJsonString(symbol);

            //Assert
            var expected = "{\"ShareName\":\"GOOG\",\"MinimumSharePriceForPeriod\":1,\"MaximumSharePriceForPeriod\":3,\"AverageSharePriceForPeriod\":2}";
            Assert.Equal(expected, jsonString);
        }
    }
}