namespace StockMarketSimulationsRest.Models
{
    public class GetMoneyFlows
    {
        public List<MoneyFlow> MoneyFlows { get; set; }
        public class MoneyFlow
        {
            public int Id { get; set; }
            public double TransactionValue { get; set; }
            public DateTime TransactionDate { get; set; }
        }
    }
}
