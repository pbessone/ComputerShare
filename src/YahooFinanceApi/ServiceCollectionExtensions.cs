using Microsoft.Extensions.DependencyInjection;
using ShareHistoryQueryApi;
using ShareHistoryQueryApi.Converters;
using YahooFinanceApi.Converters;

namespace YahooFinanceApi
{
    public static class ServiceCollectionExtensions
    {
        public static void AddYahooFinanceApi(this IServiceCollection services)
        {
            services.AddScoped<IShareHistoryQueryResponseConverter<ApiResponse>, YahooFinanceQueryResponseConverter>();
            services.AddScoped<IShareHistoryQueryRangeConverter, YahooFinanceRangeConverter>();
            services.AddHttpClient<IShareHistoryQueryService, YahooFinanceQueryService>();
        }
    }
}