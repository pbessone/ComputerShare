namespace ShareInformationServices
{
    public class BasicShareInformation
    {
        public string ShareName { get; set; }
        
        public decimal MinimumSharePriceForPeriod { get; set; }
        
        public decimal MaximumSharePriceForPeriod { get; set; }
        
        public decimal AverageSharePriceForPeriod { get; set; }
    }
}