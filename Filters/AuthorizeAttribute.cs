using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ViewAdAPI.Filters
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("api-key", out var apiKey);
            if (apiKey.ToString() != "tuYtQPg2L3oFW67aKqvzPr2y532jiZV3pdvnE7rYYm5jLbQnHmUeSNjZ1IwDY5QyckWdlXKTLcIXbzqj0ll6dAQdw2RLdL9T5mDjzx7smLKsqAn0OxwOyRdxbi8sABJm")
                context.Result = new JsonResult(new { error = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}

