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
        var tokenValue = _config[key];

        if (string.IsNullOrWhiteSpace(tokenValue) || !context.HttpContext.Request.Headers.Any(h => h.Key == key) || context.HttpContext.Request.Headers[key] != tokenValue)
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized");
        }
    }
}
