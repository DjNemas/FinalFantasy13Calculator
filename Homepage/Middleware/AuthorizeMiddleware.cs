using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Homepage.Middleware
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizeMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            context.User = new();

            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                // Prüfen, ob der Header existiert
                if (context.Session.GetString("miku") is string bearerToken)
                {
                    var user = await authService.GetUser(bearerToken);
                    if (user.Success)
                    {
                        // Claims erstellen
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                            new Claim(ClaimTypes.Role, Enum.GetName(user.Data.Role)!)
                        };

                        // ClaimsPrincipal erstellen und setzen
                        var identity = new ClaimsIdentity(claims, "FFXIIIScheme");
                        context.User = new ClaimsPrincipal(identity);
                    }
                }
            }
            context.Response.StatusCode = 200;
            await _next(context);
        }
    }
}
