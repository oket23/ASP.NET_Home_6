using Microsoft.AspNetCore.Mvc.Filters;

namespace Home_6.API.Filters;

public class AuthFilter: Attribute, IAuthorizationFilter
{
    private const string ValidApiKey = "1234";
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var key))
        {
            throw new UnauthorizedAccessException("X-API-KEY is missing in headers");
        }
        
        if (key != ValidApiKey)
        {
            throw new UnauthorizedAccessException("Invalid API Key");
        }
    }
}