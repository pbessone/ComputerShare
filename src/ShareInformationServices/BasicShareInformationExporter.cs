using System.Text.Json;
using System.Threading.Tasks;

namespace ShareInformationServices
{
    public class BasicShareInformationExporter : IBasicShareInformationExporter
    {
        private readonly IBasicShareInformationService _basicShareInformationService;

        public BasicShareInformationExporter(IBasicShareInformationService basicShareInformationService)
        {
            _basicShareInformationService = basicShareInformationService;
        }

        public async Task<string> GetAsJsonString(string symbol)
        {
            var shareInformation = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
            return ConvertToJsonString(shareInformation);
        }
        
        private string ConvertToJsonString(BasicShareInformation shareInformation)
        {
            var jsonString = JsonSerializer.Serialize(shareInformation);
            return jsonString;
        }
    }
}