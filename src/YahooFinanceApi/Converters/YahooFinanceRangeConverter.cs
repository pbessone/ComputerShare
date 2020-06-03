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
                    return "1d";
                case ShareHistoryQueryRange.OneWeek:
                    return "5d";
                case ShareHistoryQueryRange.OneMonth:
                    return "1mo";
                case ShareHistoryQueryRange.ThreeMonths:
                    return "3mo";
                default:
                    throw new NotSupportedException("The range requested is not supported by this api.");
            }
        }
    }
}