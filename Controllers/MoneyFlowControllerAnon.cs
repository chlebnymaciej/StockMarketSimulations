//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using StockMarketSimulationsRest.Models;
//using StockMarketSimulationsRest.Storage.Services;

//namespace StockMarketSimulationsRest.Controllers
//{
//    [ApiController]
//    [AllowAnonymous]
//    [Route("moneyflow-anon")]
//    public class MoneyFlowAnonController : ControllerBase
//    {
//        private TransactionService TransactionService { get; set; }
//        private MoneyFlowService MoneyFlowService { get; set; }
//        public MoneyFlowAnonController(TransactionService transactionService, MoneyFlowService moneyFlowService)
//        {
//            TransactionService = transactionService;
//            MoneyFlowService = moneyFlowService;
//        }

//        [HttpGet("{userId}")]
//        public ActionResult<GetMoneyFlows> Get(string userId)
//        {
//            var data = MoneyFlowService.Read(userId).Select(x => new GetMoneyFlows.MoneyFlow
//            {
//                Id = x.TransactionId,
//                TransactionDate = x.TransactionDate,
//                TransactionValue = x.TransactionValue * (x.IsWithdraw ? -0.01 : 0.01)
//            });

//            return Ok(new GetMoneyFlows
//            {
//                MoneyFlows = data.ToList()
//            });
//        }

//        [HttpPost("{userId}/add/{value}")]
//        public IActionResult PostBuyStocks(string userId, long value)
//        {
//            MoneyFlowService.Add(new Storage.Models.MoneyFlow
//            {
//                UserId = userId,
//                IsWithdraw = false,
//                TransactionValue = (ulong)(value * 100)
//            });
//            return NoContent();
//        }

//        [HttpPost("{userId}/withdraw/{value}")]
//        public IActionResult PostSellStocks(string userId, long value)
//        {
//            var dataTransaction = TransactionService.Read(userId).Select(x => x.TransactionValue * (x.IsBuy ? -0.01 : 0.01));
//            var dataWalletChanges = MoneyFlowService.Read(userId).Select(x => x.TransactionValue * (x.IsWithdraw ? -0.01 : 0.01));
//            var currentState = dataTransaction.Sum() + dataWalletChanges.Sum();

//            if (currentState < value)
//                return BadRequest("Insuficient amount of money on account");

//            MoneyFlowService.Add(new Storage.Models.MoneyFlow
//            {
//                UserId = userId,
//                IsWithdraw = true,
//                TransactionValue = (ulong)(value * 100)
//            });
//            return NoContent();
//        }
//    }
//}
