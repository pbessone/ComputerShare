using System.Threading.Tasks;

namespace ShareInformationServices
{
    public interface IBasicShareInformationExporter
    {
        Task<string> GetAsJsonString(string symbol);
    }
}