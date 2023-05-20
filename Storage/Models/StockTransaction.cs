namespace StockMarketSimulationsRest.Storage.Models
{
    public class StockTransaction : BaseModel
    {
        public ulong StockCount { get; set; }
        public string StockId { get; set; }
        public ulong StockPriceAtMoment { get; set; }
        public bool IsBuy { get; set; }
    }
}
