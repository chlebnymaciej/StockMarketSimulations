using Microsoft.AspNetCore.Mvc;

namespace StockMarketSimulationsRest
{
    public static class ControllerBaseExtension
    {
        public static string GetUserId(this ControllerBase controllerBase)
        {
            if (controllerBase.User == null)
                return "2884a694-6a60-4e87-9477-6bd589106ab2";
            return controllerBase.User.Claims.First(c => c.Type == "UserId").Value;
        }
    }
}
