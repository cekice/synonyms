using Microsoft.AspNetCore.Builder;

namespace Middleware.GlobalExeptionHandler
{
    public static class ExeptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
