
using LVT.Services.Middleware;
using Microsoft.AspNetCore.Builder;


namespace LVT.Services.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
