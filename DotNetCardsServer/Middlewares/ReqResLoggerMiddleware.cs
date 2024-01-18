using Microsoft.AspNetCore.Http.Extensions;

namespace DotNetCardsServer.Middlewares
{
    public class ReqResLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public ReqResLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ReqResLoggerMiddleware> logger)
        {
            string origin = context.Request.GetDisplayUrl();
            string path = context.Request.Path;
            string method = context.Request.Method;
            DateTime dateTime = DateTime.Now;

            await _next(context);

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - dateTime;
            string statusCode = context.Response.StatusCode.ToString();
            string responseTime = duration.TotalMilliseconds.ToString();

            logger.LogInformation($"[{dateTime}] - {origin} - {path} - {method} - {statusCode} - {responseTime}");
        }
    }

}
