namespace YahooFinanceApi
{
    public class ChartResultMeta
    {
        public string Currency { get; set; }
        
        public string Symbol { get; set; }
        
        public string ExchangeName { get; set; }
        
        public string InstrumentType { get; set; }
        
        public long? FirstTradeDate { get; set; }
        
        public long? RegularMarketTime { get; set; }
        
        public int GmtOffset { get; set; }
        
        public string TimeZone { get; set; }
        
        public string ExchangeTimeZoneName { get; set; }
        
        public decimal RegularMarketPrice { get; set; }
        
        public decimal ChartPreviousClose { get; set; }
        
        public int PriceHint { get; set; }
        
        public string[] ValidRanges { get; set; }
    }
}