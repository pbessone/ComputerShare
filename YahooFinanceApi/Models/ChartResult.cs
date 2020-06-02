namespace YahooFinanceApi
{
    public class ChartResult
    {
        public ChartResultMeta Meta { get; set; }
        
        public long[] TimeStamp { get; set; }
        
        public ChartResultIndicator Indicators { get; set; }
    }
}