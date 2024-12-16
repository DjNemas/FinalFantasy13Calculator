using Microsoft.AspNetCore.Authorization;
using RestAPI.Services;
using System.Security.Claims;

namespace RestAPI.Middlewares
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public AuthorizeMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                if (endpoint.Metadata.FirstOrDefault(x => x.GetType() == typeof(AuthorizeAttribute)) is not null)
                {
                    var isAuthorized = false;

                    // Prüfen, ob der Header existiert
                    if (context.Request.Headers.Authorization.FirstOrDefault() is string bearerToken)
                    {
                        if (bearerToken.StartsWith("Bearer "))
                        {
                            bearerToken = bearerToken.Substring(7);
                            if (bearerToken.Equals(_config.GetSection("API:Token").Value))
                            {
                                // Claims erstellen
                                var claims = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, "3939"),
                                    new Claim(ClaimTypes.Role, "Administrator")
                                };

                                // ClaimsPrincipal erstellen und setzen
                                var identity = new ClaimsIdentity(claims, "OCPPScheme");
                                context.User = new ClaimsPrincipal(identity);

                                isAuthorized = true;
                            }
                                
                        }
                    }

                    if (!isAuthorized)
                    {

                        context.Response.ContentType = "text/plain";
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Unauthorized");
                        return;
                    }
                    else
                    {
                        context.Response.StatusCode = 200;
                    }
                }
            }
            await _next(context);
        }
    }
}
