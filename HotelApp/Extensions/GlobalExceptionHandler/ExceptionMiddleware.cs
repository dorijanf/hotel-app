using System;
using System.Net;
using System.Threading.Tasks;
using HotelApp.API.Extensions.Exceptions;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HotelApp.API.Extensions.GlobalExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IWebHostEnvironment env)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex, env);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment env)
        {
            HttpStatusCode statusCode;
            string message = exception.Message;
            var exceptionType = exception.GetType();
            var stackTrace = String.Empty;

            if (exceptionType == typeof(BadRequestException))
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else if(exceptionType == typeof(NotAuthorizedException))
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                if (env.IsEnvironment("Development"))
                    stackTrace = exception.StackTrace;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(new ResponseDTO()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }

    }
}
