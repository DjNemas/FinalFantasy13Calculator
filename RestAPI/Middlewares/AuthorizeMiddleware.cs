using Microsoft.AspNetCore.Authorization;
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

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
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
                            if (await authService.GetSessionByBearerTokenAsync(bearerToken, true) is Session session)
                            {
                                if(session.ExpirationDateBearerToken.UtcDateTime > DateTime.UtcNow)
                                {

                                    // Claims erstellen
                                    var claims = new List<Claim>()
                                    {
                                        new Claim(ClaimTypes.NameIdentifier, session.User.Id.ToString()),
                                        new Claim(ClaimTypes.Role, Enum.GetName(session.User.Role.Role)!)
                                    };

                                    // ClaimsPrincipal erstellen und setzen
                                    var identity = new ClaimsIdentity(claims, "FFXIIIScheme");
                                    context.User = new ClaimsPrincipal(identity);

                                    isAuthorized = true;
                                }
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
