using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Home_6.API.Filters;

public class LogFilter : IActionFilter
{
    private Stopwatch? _stopwatch;
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
        
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<LogFilter>>();
        
        logger.LogInformation("[Log] -> Started: {RequestMethod} {RequestPath}", context.HttpContext.Request.Method, context.HttpContext.Request.Path);

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (_stopwatch != null)
        {
            _stopwatch.Stop();
            
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<LogFilter>>();
            var elapsed = _stopwatch.ElapsedMilliseconds;

            logger.LogInformation("[Log] <- Finished: {Path} | Status: {StatusCode} | Time: {Time} ms", 
                context.HttpContext.Request.Path,
                context.HttpContext.Response.StatusCode, 
                elapsed);
        }
    }
}