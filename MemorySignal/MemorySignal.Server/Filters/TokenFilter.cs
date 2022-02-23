using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MemorySignal.Server.Filters;
public class TokenFilter : IActionFilter
{
    private readonly IConfiguration _config;

    public TokenFilter(IConfiguration config)
    {
        _config = config;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        const string key = "Token";
        if (!context.HttpContext.Request.Headers.Any(h => h.Key == key) || context.HttpContext.Request.Headers[key] != _config[key])
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized");
        }
    }
}
