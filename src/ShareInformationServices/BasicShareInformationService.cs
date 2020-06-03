using System;
using System.Threading.Tasks;
using ShareHistoryQueryApi;

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
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }
            
            var range = ShareHistoryQueryRange.OneWeek;
            var response = await _shareHistoryQueryService.Query(symbol, range);

            var history = response.History;
            var maximumSharePriceForPeriod = CalculateMaximumSharePrice(history.High);
            var minimumSharePriceForPeriod = CalculateMinimumSharePrice(history.Low);
            var averageSharePriceForPeriod = CalculateAverageSharePrice(history);
            
            var shareInformation = new BasicShareInformation
            {
                ShareName = response.ShareName,
                MaximumSharePriceForPeriod = maximumSharePriceForPeriod,
                MinimumSharePriceForPeriod = minimumSharePriceForPeriod,
                AverageSharePriceForPeriod = averageSharePriceForPeriod
            };
            
            return shareInformation;
        }

        private decimal CalculateMaximumSharePrice(decimal?[] quoteIndicatorsHigh)
        {
            decimal maxSharePrice = 0;
            foreach (var sharePrice in quoteIndicatorsHigh)
            {
                if (sharePrice != null && sharePrice > maxSharePrice)
                {
                    maxSharePrice = sharePrice.Value;
                }
            }
            
            return maxSharePrice;
        }

        private decimal CalculateMinimumSharePrice(decimal?[] quoteIndicatorsLow)
        {
            var minSharePrice = decimal.MaxValue;
            foreach (var sharePrice in quoteIndicatorsLow)
            {
                if (sharePrice != null && sharePrice < minSharePrice)
                {
                    minSharePrice = sharePrice.Value;
                }
            }
            
            // The initial min value wouldn't have changed if there were no low quote indicators, so just return 0
            if (minSharePrice == decimal.MaxValue)
            {
                return 0;
            }
            
            return minSharePrice;
        }
        
        private decimal CalculateAverageSharePrice(ShareHistory history)
        {
            decimal sum = 0;
            int count = 0;
            for (int x = 0; x < history.TimeStamp.Length; x++)
            {
                Add(history.Close[x], ref sum, ref count);
                Add(history.High[x], ref sum, ref count);
                Add(history.Low[x], ref sum, ref count);
                Add(history.Open[x], ref sum, ref count);
            }

            // If nothing was counted just return 0
            if (count == 0)
            {
                return 0;
            }

            decimal average = sum / count;
            return average;
        }

        private static void Add(decimal? sharePrice, ref decimal sum, ref int count)
        {
            if (sharePrice != null)
            {
                sum += sharePrice.Value;
                count++;
            }
        }
    }
}