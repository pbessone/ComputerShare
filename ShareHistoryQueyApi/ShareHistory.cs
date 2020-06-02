namespace ShareHistoryQueyApi
{
    public class ShareHistory
    {
        public long[] TimeStamp { get; set; }
        
        public double?[] Open { get; set; }

        public double?[] Low { get; set; }
        
        public double?[] High { get; set; }
        
        public double?[] Close { get; set; }
    }
}