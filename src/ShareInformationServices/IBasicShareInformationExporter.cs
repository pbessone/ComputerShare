using System.Threading.Tasks;

namespace ShareInformationServices
{
    public interface IBasicShareInformationExporter
    {
        string GetJsonString(BasicShareInformation shareInformation);
    }
}