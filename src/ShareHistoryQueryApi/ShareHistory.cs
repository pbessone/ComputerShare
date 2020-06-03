namespace ShareHistoryQueryApi
{
    public class ShareHistory
    {
        public ShareHistory()
        {
            TimeStamp = new long[] {};
            Open = new decimal?[] {};
            Low = new decimal?[] {};
            High = new decimal?[] {};
            Close = new decimal?[] {};
        }
        
        public long[] TimeStamp { get; set; }
        
        public decimal?[] Open { get; set; }

        public decimal?[] Low { get; set; }
        
        public decimal?[] High { get; set; }
        
        public decimal?[] Close { get; set; }
    }
}