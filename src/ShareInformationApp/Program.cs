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
            
            // Run application loop
            var application = serviceProvider.GetService<MainApplication>();
            await application.RunAsync();
        }
        
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddYahooFinanceApi();
            services.AddScoped<IBasicShareInformationService, BasicShareInformationService>();
            services.AddScoped<IBasicShareInformationExporter, BasicShareInformationExporter>();
            services.AddScoped<MainApplication>();
            
            return services;
        }
    }
}