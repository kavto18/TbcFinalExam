using System;
using System.Threading.Tasks;
using Common.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace TbcExamFinalTest.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.Info($"Request Logging {context.Connection.RemoteIpAddress} {context.Request.Path} ");
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception Catched In ExceptionLoggingMiddleware {ex}");
                context.Response.Clear();
                await context.Response.WriteAsync(ex.Message);
                return;
            }
        }
    }

    public static class ExceptionLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLoggingMiddleware>();
        }
    }
}
