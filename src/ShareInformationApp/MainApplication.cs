using System;
using System.Linq;
using System.Threading.Tasks;
using ShareHistoryQueryApi.Exceptions;
using ShareInformationServices;

namespace ShareInformationApp
{
    public class MainApplication
    {
        private readonly IBasicShareInformationService _basicShareInformationService;
        private readonly IBasicShareInformationExporter _basicShareInformationExporter;

        public MainApplication(
            IBasicShareInformationService basicShareInformationService,
            IBasicShareInformationExporter basicShareInformationExporter)
        {
            _basicShareInformationService = basicShareInformationService;
            _basicShareInformationExporter = basicShareInformationExporter;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Welcome to the ShareInformation Application");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            var yesNoOptions = new[] {"Y", "N"};
            var shouldContinue = true;  
            while (shouldContinue)
            {
                var shareSymbol = GetUserInput("Please type the name of the share (example: GOOG, AAPL, CSCO):", "Please enter a valid share symbol");
                    
                var currentShareInformation = await GetShareInformation(shareSymbol);
                if (currentShareInformation != null)
                {
                    PrintShareInformation(currentShareInformation);

                    var exportChoice = GetUserInput("Do you want to export this share information as JSON?", "Not a valid option", yesNoOptions);
                    if (exportChoice == "Y")
                    {
                        ExportShareInformation(currentShareInformation);
                    }
                }

                var continueChoice = GetUserInput("Do you want to query another share?", "Not a valid option", yesNoOptions);
                if (continueChoice == "N")
                {
                    shouldContinue = false;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Thank you for using the ShareInformation Application");
        }

        private async Task<BasicShareInformation> GetShareInformation(string symbol)
        {
            try
            {
                var info = await _basicShareInformationService.GetBasicShareInformationAsync(symbol);
                return info;
            }
            catch (InvalidRequestException ex)
            {
                Console.WriteLine("There has been an issue fetching the share information");
                Console.WriteLine(ex.Code);
                Console.WriteLine(ex.Message);
            }
            catch
            {
                Console.WriteLine("There has been an unexpected issue fetching the share information.");
            }

            return null;
        }
        
        private void PrintShareInformation(BasicShareInformation info)
        {
            Console.WriteLine("Share Name: {0}", info.ShareName);
            Console.WriteLine("Minimum Share Price: {0}", info.MinimumSharePriceForPeriod);
            Console.WriteLine("Maximum Share Price: {0}", info.MaximumSharePriceForPeriod);
            Console.WriteLine("Average Share Price: {0}", info.AverageSharePriceForPeriod);
        }

        private void ExportShareInformation(BasicShareInformation info)
        {
            var jsonString = _basicShareInformationExporter.GetJsonString(info);
            Console.WriteLine(jsonString);            
        }

        private string GetUserInput(string inputMessage, string errorMessage, string[] validOptions = null)
        {
            while (true)
            {
                if (validOptions != null && validOptions.Length > 0)
                {
                    Console.Write("{0} ({1}) ", inputMessage, string.Join('/', validOptions));
                }
                else
                {
                    Console.Write("{0} ", inputMessage);
                }

                var userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput) ||
                    (validOptions != null && validOptions.Length > 0 && !validOptions.Contains(userInput)))
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }
                
                return userInput;
            }            
        }
    }
}