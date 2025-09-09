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
        private static readonly PathString RelativeLoginPath = new("/Security/Login");

        public AuthTokenMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            PathString path = context.Request.Path;        // p. ej. "/Security/Login" o "/farmacheckPortalWeb/..."
            PathString basePath = context.Request.PathBase; // p. ej. "/farmacheckPortalWeb" (vacío en local)
            PathString loginPath = basePath.Add(RelativeLoginPath); // "/farmacheckPortalWeb/Security/Login" (o solo "/Security/Login")

            // Helper: ¿la ruta actual empieza con un segmento (con o sin base)?
            bool On(string segment)
            {
                var seg = new PathString(segment);
                return path.StartsWithSegments(seg, out _)
                    || path.StartsWithSegments(basePath.Add(seg), out _);
            }

            // Allowlist: login, endpoints de auth, swagger, health, archivos estáticos
            if (On("/Security/Login") ||
                On("/Auth") ||
                On("/swagger") ||
                On("/health") ||
                (path.HasValue && Path.HasExtension(path.Value))) // .css, .js, .png, etc.
            {
                await _next(context);
                return;
            }

            // ¿Trae cookie o header Bearer?
            bool hasCookie = context.Request.Cookies.ContainsKey(AuthCookie);
            bool hasBearer = context.Request.Headers.TryGetValue("Authorization", out var hdr)
                             && hdr.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase);

            if (!hasCookie && !hasBearer)
            {
                // Evitar loop si ya estamos en el login
                if (path.Equals(RelativeLoginPath) || path.Equals(loginPath))
                {
                    await _next(context);
                    return;
                }

                context.Response.Redirect(loginPath.Value); // respeta el PathBase
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
