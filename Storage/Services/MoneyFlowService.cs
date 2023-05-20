
using StockMarketSimulationsRest.Storage.Models;

namespace StockMarketSimulationsRest.Storage.Services
{
    public class MoneyFlowService : BaseService<MoneyFlow>
    {
        public MoneyFlowService(DataContext context) : base(context)
        {
        }
    }
}
