using System.Threading.Tasks;

namespace ShareInformationServices
{
    public interface IBasicShareInformationService
    {
        Task<BasicShareInformation> GetBasicShareInformationAsync(string symbol);
    }
}