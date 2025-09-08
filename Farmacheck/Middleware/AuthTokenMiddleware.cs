using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace Farmacheck.Middleware
{
    public class AuthTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private const string AuthCookie = "AuthToken";
        private const string LoginPath = "/Security/Login";

        public AuthTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            if (path.StartsWithSegments(LoginPath, System.StringComparison.OrdinalIgnoreCase) ||
                path.StartsWithSegments("/Auth", System.StringComparison.OrdinalIgnoreCase) ||
                Path.HasExtension(path))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Cookies.ContainsKey(AuthCookie))
            {
                context.Response.Redirect(LoginPath);
                return;
            }

            await _next(context);
        }
    }

    public static class AuthTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthTokenMiddleware>();
        }
    }
}
