﻿namespace YahooFinanceApi
{
    public class ChartResultIndicatorQuote
    {
        public decimal?[] Open { get; set; }
        public decimal?[] Low { get; set; }
        public decimal?[] High { get; set; }
        
        public decimal?[] Volume { get; set; }
        public decimal?[] Close { get; set; }
    }
}