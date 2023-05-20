namespace StockMarketSimulationsRest.Storage.Models
{
    public class BaseModel
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; }
        public ulong TransactionValue { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
