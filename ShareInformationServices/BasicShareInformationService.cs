using System.Threading.Tasks;
using ShareHistoryQueyApi;

namespace ShareInformationServices
{
    public class BasicShareInformationService : IBasicShareInformationService
    {
        private readonly IShareHistoryQueryService _shareHistoryQueryService;

        public BasicShareInformationService(IShareHistoryQueryService shareHistoryQueryService)
        {
            _shareHistoryQueryService = shareHistoryQueryService;
        }
        
        public async Task<BasicShareInformation> GetBasicShareInformationAsync(string symbol)
        {
            var range = ShareHistoryQueryRange.OneWeek;

            var response = await _shareHistoryQueryService.Query(symbol, range);

            var history = response.History;
            var minimumSharePriceForPeriod = CalculateMinimumSharePrice(history.Low);
            var maximumSharePriceForPeriod = CalculateMaximumSharePrice(history.High);
            var averageSharePriceForPeriod = CalculateAverageSharePrice(history);
            
            var shareInformation = new BasicShareInformation
            {
                ShareName = response.ShareName,
                MinimumSharePriceForPeriod = minimumSharePriceForPeriod,
                MaximumSharePriceForPeriod = maximumSharePriceForPeriod,
                AverageSharePriceForPeriod = averageSharePriceForPeriod
            };
            
            return shareInformation;
        }

        private double CalculateMinimumSharePrice(double?[] quoteIndicatorsLow)
        {
            double minValue = double.MaxValue;
            foreach (var value in quoteIndicatorsLow)
            {
                if (value != null && value < minValue)
                {
                    minValue = value.Value;
                }
            }
            
            return minValue;
        }
        
        private double CalculateMaximumSharePrice(double?[] quoteIndicatorsHigh)
        {
            double maxValue = 0;
            foreach (var value in quoteIndicatorsHigh)
            {
                if (value != null && value > maxValue)
                {
                    maxValue = value.Value;
                }
            }
            
            return maxValue;
        }
        
        private double CalculateAverageSharePrice(ShareHistory quoteIndicators)
        {
            double sum = 0;
            int count = 0;
            for (int x = 0; x < quoteIndicators.Close.Length; x++)
            {
                Add(quoteIndicators.Close[x], ref sum, ref count);
                Add(quoteIndicators.High[x], ref sum, ref count);
                Add(quoteIndicators.Low[x], ref sum, ref count);
                Add(quoteIndicators.Open[x], ref sum, ref count);
            }

            double average = sum / count;
            return average;
        }

        private static void Add(double? sharePrice, ref double sum, ref int count)
        {
            if (sharePrice != null)
            {
                sum += sharePrice.Value;
                count++;
            }
        }
    }
}