using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarketSimulationsRest.Models;
using StockMarketSimulationsRest.Storage.Services;

namespace StockMarketSimulationsRest.Controllers
{
    [ApiController]
    [Route("transaction")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private TransactionService TransactionService { get; set; }
        private MoneyFlowService MoneyFlowService { get; set; }
        public TransactionController(TransactionService transactionService, MoneyFlowService moneyFlowService)
        {
            TransactionService = transactionService;
            MoneyFlowService = moneyFlowService;
        }

        [HttpGet("")]
        public ActionResult<GetTransactions> Get()
        {
            var userId = this.GetUserId();
            var data = TransactionService.Read(userId).Select(x => new GetTransactions.Transaction
            {
                Id = x.TransactionId,
                StockCount = x.StockCount,
                StockName = x.StockId,
                TransactionValue = x.TransactionValue * (x.IsBuy ? -0.01 : 0.01)
            });
            return Ok(new GetTransactions
            {
                Transactions = data.ToList()
            });
        }

        [HttpPost("buy")]
        public IActionResult PostBuyStocks(PostNewTransaction request)
        {
            var userId = this.GetUserId();
            var dataTransaction = TransactionService.Read(userId).Select(x => x.TransactionValue * (x.IsBuy ? -0.01 : 0.01));
            var dataWalletChanges = MoneyFlowService.Read(userId).Select(x => x.TransactionValue * (x.IsWithdraw ? -0.01 : 0.01));
            var currentState = dataTransaction.Sum() + dataWalletChanges.Sum();

            if (currentState < request.StockCount * request.StockPrice)
                return BadRequest("Insuficient amount of money on account");
            if (request.StockCount < 1)
                return BadRequest("Stock count to low");

            TransactionService.Add(new Storage.Models.StockTransaction
            {
                IsBuy = true,
                UserId = userId,
                StockCount = request.StockCount,
                StockId = request.StockId,
                StockPriceAtMoment = (ulong)(request.StockPrice * 100),
                TransactionValue = (ulong)(request.StockPrice * 100 * request.StockCount)
            });
            return NoContent();
        }

        [HttpPost("sell")]
        public IActionResult PostSellStocks(PostNewTransaction request)
        {
            var userId = this.GetUserId();
            var stockCount = TransactionService.Read(userId).Where(x => x.StockId == request.StockId).Select(y => (long)y.StockCount * (y.IsBuy ? 1 : -1)).Sum();

            if (stockCount < (long)request.StockCount)
                return BadRequest("Insuficient amount of stocks on account");
            if (request.StockCount < 1)
                return BadRequest("Stock count to low");

            TransactionService.Add(new Storage.Models.StockTransaction
            {
                IsBuy = false,
                UserId = userId,
                StockCount = request.StockCount,
                StockId = request.StockId,
                StockPriceAtMoment = (ulong)(request.StockPrice * 100),
                TransactionValue = (ulong)(request.StockPrice * 100 * request.StockCount)
            });
            return NoContent();
        }
    }
}
