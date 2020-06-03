using System.Threading.Tasks;
using Moq;
using ShareInformationServices;
using Xunit;

namespace ShareInformationServicesTests
{
    public class BasicShareInformationExporterTests
    {
        private readonly BasicShareInformationExporter _basicShareInformationExporter;

        public BasicShareInformationExporterTests()
        {
            _basicShareInformationExporter = new BasicShareInformationExporter();            
        }
        
        [Fact]
        public void GetAsJsonString_CanFetchBasicShareInformation_ReturnJsonString()
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
            
            // Act
            var jsonString = _basicShareInformationExporter.GetJsonString(basicShareInformation);

            //Assert
            var expected = "{\"ShareName\":\"GOOG\",\"MinimumSharePriceForPeriod\":1,\"MaximumSharePriceForPeriod\":3,\"AverageSharePriceForPeriod\":2}";
            Assert.Equal(expected, jsonString);
        }
    }
}