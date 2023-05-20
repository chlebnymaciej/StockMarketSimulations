using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarketSimulationsRest.Models;
using StockMarketSimulationsRest.Storage.Services;

namespace StockMarketSimulationsRest.Controllers
{
    [ApiController]
    [Route("moneyflow")]
    [Authorize]
    public class MoneyFlowController : ControllerBase
    {
        private TransactionService TransactionService { get; set; }
        private MoneyFlowService MoneyFlowService { get; set; }
        public MoneyFlowController(TransactionService transactionService, MoneyFlowService moneyFlowService)
        {
            TransactionService = transactionService;
            MoneyFlowService = moneyFlowService;
        }

        [HttpGet("")]
        public ActionResult<GetMoneyFlows> Get()
        {
            var userId = this.GetUserId();
            var data = MoneyFlowService.Read(userId).Select(x => new GetMoneyFlows.MoneyFlow
            {
                Id = x.TransactionId,
                TransactionDate = x.TransactionDate,
                TransactionValue = x.TransactionValue * (x.IsWithdraw ? -0.01 : 0.01)
            });

            return Ok(new GetMoneyFlows
            {
                MoneyFlows = data.ToList()
            });
        }

        [HttpPost("add/{value}")]
        public IActionResult PostBuyStocks(long value)
        {
            var userId = this.GetUserId();
            MoneyFlowService.Add(new Storage.Models.MoneyFlow
            {
                UserId = userId,
                IsWithdraw = false,
                TransactionValue = (ulong)(value * 100)
            });
            return NoContent();
        }

        [HttpPost("withdraw/{value}")]
        public IActionResult PostSellStocks(long value)
        {
            var userId = this.GetUserId();
            var dataTransaction = TransactionService.Read(userId).Select(x => x.TransactionValue * (x.IsBuy ? -0.01 : 0.01));
            var dataWalletChanges = MoneyFlowService.Read(userId).Select(x => x.TransactionValue * (x.IsWithdraw ? -0.01 : 0.01));
            var currentState = dataTransaction.Sum() + dataWalletChanges.Sum();

            if (currentState < value)
                return BadRequest("Insuficient amount of money on account");

            MoneyFlowService.Add(new Storage.Models.MoneyFlow
            {
                UserId = userId,
                IsWithdraw = true,
                TransactionValue = (ulong)(value * 100)
            });
            return NoContent();
        }
    }
}
