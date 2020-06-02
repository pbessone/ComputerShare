using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ShareInformationServices;
using YahooFinanceApi;

namespace ShareInformationApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Initialise DI container
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            
            var service = serviceProvider.GetService<IBasicShareInformationService>();
            var info = await service.GetBasicShareInformationAsync("GOOG");
            
            Console.WriteLine("Share Name: {0}", info.ShareName);
            Console.WriteLine("Minimum Share Price: {0}", info.MinimumSharePriceForPeriod);
            Console.WriteLine("Maximum Share Price: {0}", info.MaximumSharePriceForPeriod);
            Console.WriteLine("Average Share Price: {0}", info.AverageSharePriceForPeriod);
            
            var exporter = serviceProvider.GetService<IBasicShareInformationExporter>();
            var jsonString = await exporter.GetAsJsonString("GOOG");
            Console.WriteLine("JSON: {0}", jsonString);
        }
        
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddYahooFinanceApi();
            services.AddScoped<IBasicShareInformationService, BasicShareInformationService>();
            services.AddScoped<IBasicShareInformationExporter, BasicShareInformationExporter>();
            
            return services;
        }
    }
}