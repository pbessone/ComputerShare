using System;
using ShareHistoryQueryApi;
using ShareHistoryQueryApi.Converters;

namespace YahooFinanceApi.Converters
{
    public class YahooFinanceRangeConverter : IShareHistoryQueryRangeConverter
    {
        public string ConvertFrom(ShareHistoryQueryRange range)
        {
            switch (range)
            {
                case ShareHistoryQueryRange.OneDay:
                    return YahooRange.OneDay;
                case ShareHistoryQueryRange.OneWeek:
                    return YahooRange.FiveDays;
                case ShareHistoryQueryRange.OneMonth:
                    return YahooRange.OneMonth;
                case ShareHistoryQueryRange.ThreeMonths:
                    return YahooRange.ThreeMonths;
                default:
                    throw new NotSupportedException("The range requested is not supported by this api.");
            }
        }
    }
}