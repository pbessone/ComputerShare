using Microsoft.Extensions.DependencyInjection;
using ShareHistoryQueyApi;
using ShareHistoryQueyApi.Converters;
using YahooFinanceApi.Converters;

namespace YahooFinanceApi
{
    public static class ServiceCollectionExtensions
    {
        public static void AddYahooFinanceApi(this IServiceCollection services)
        {
            services.AddScoped<IShareHistoryQueryResponseConverter<ApiResponse>, YahooFinanceQueryResponseConverter>();
            services.AddScoped<IShareHistoryQueryRangeConverter, YahooFinanceRangeConverter>();
            services.AddHttpClient<IShareHistoryQueryService, YahooFinanceQueryQueryService>();
        }
    }
}