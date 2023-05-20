
using StockMarketSimulationsRest.Storage.Models;

namespace StockMarketSimulationsRest.Storage.Services
{
    public class TransactionService : BaseService<StockTransaction>
    {
        public TransactionService(DataContext context) : base(context)
        {
        }
    }
}
