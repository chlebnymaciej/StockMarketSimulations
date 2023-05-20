namespace StockMarketSimulationsRest.Models
{
    public class PostNewTransaction
    {
        public string StockId { get; set; }
        public ulong StockCount { get; set; }
        public double StockPrice { get; set; }
    }
}
