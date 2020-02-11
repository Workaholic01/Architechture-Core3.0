using System;
using System.Net;
using System.Threading.Tasks;
using LVT.Models.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LVT.Services.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public CustomExceptionMiddleware(RequestDelegate next , IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex);
            }   
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ErrorDetails errorDetails = new ErrorDetails();
            errorDetails.StatusCode = context.Response.StatusCode;

            if (_configuration.GetSection("IsDevelopmentEnvironment").Value == "1")
                errorDetails.Message = ex.ToString();
            else
                errorDetails.Message = "Internal Server Error from the custom middleware.";
            
            return context.Response.WriteAsync(errorDetails.ToString());
        }
    }
  
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
