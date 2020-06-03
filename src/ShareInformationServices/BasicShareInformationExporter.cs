using System.Text.Json;
using System.Threading.Tasks;

namespace ShareInformationServices
{
    public class BasicShareInformationExporter : IBasicShareInformationExporter
    {
        public string GetJsonString(BasicShareInformation shareInformation)
        {
            var jsonString = JsonSerializer.Serialize(shareInformation);
            return jsonString;
        }
    }
}