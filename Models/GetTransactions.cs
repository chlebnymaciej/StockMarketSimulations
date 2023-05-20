namespace StockMarketSimulationsRest.Models
{
    public class GetTransactions
    {
        public List<Transaction> Transactions { get; set; }
        public class Transaction
        {
            public int Id { get; set; }
            public string StockName { get; set; }
            public double TransactionValue { get; set; }
            public ulong StockCount { get; set; }
        }
    }
}
