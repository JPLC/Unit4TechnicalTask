using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Toolkit.Api.Middlewares
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(LogActionFilter));

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.Info($"Enter Controller: {context.RouteData.Values["controller"]} - Action: { context.RouteData.Values["action"]}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.Info($"Exit Controller: {context.RouteData.Values["controller"]} - Action: { context.RouteData.Values["action"]}");
        }
    }
}
