using System.Diagnostics;
using API.Data.Models;
using API.Interfaces;

namespace API.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            var watch = Stopwatch.StartNew();
            await _next.Invoke(context);
            watch.Stop();

            var routeData = context.GetRouteData();
            _logger.LogInformation("{duration}ms", watch.ElapsedMilliseconds);
            _logger.LogInformation("{route} - route", routeData.Values);
            _logger.LogInformation("{ip} - IP", context.Request.Headers["x-forwarded-for"]);

            if (context.User.Claims.Count() != 0)
            {
                _logger.LogInformation("{user} - user", context.User.Claims.First().Value);
                Logging logging = new Logging { RequestedRoute = (string)routeData.Values["action"], RequestedController = (string)routeData.Values["controller"], UserGuid = Guid.Parse(context.User.Claims.First().Value), Runtime = watch.ElapsedMilliseconds, clientIP = context.Request.Headers["x-forwarded-for"] };
                unitOfWork.LoggingRepository.SaveLogToDBAsync(logging);
                await unitOfWork.Complete();
            }
            else
            {
                Logging logging = new Logging { RequestedRoute = (string)routeData.Values["action"], RequestedController = (string)routeData.Values["controller"], Runtime = watch.ElapsedMilliseconds, clientIP = context.Request.Headers["x-forwarded-for"] };
                unitOfWork.LoggingRepository.SaveLogToDBAsync(logging);
                await unitOfWork.Complete();
            }
        }
    }
}