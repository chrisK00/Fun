using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MemorySignal.Server.Filters;
public class TokenFilter : IActionFilter
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _environment;
    const string _key = "Token";

    public TokenFilter(IConfiguration config, IWebHostEnvironment environment)
    {
        _config = config;
        _environment = environment;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    // borde istället ha en identity server och SSO tbh eller iaf google sign in enklare
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (_environment.IsDevelopment())
        {
            return;
        }

        var tokenValue = _config[_key];

        if (string.IsNullOrWhiteSpace(tokenValue) 
            || !context.HttpContext.Request.Headers.Any(h => h.Key == _key) 
            || context.HttpContext.Request.Headers[_key].First().Trim() != tokenValue.Trim())
        {
            context.Result = new UnauthorizedObjectResult($"Key: {context.HttpContext.Request.Headers[_key]} {context.HttpContext.Request.Headers[_key].First().Trim()}");
        }
    }
}
