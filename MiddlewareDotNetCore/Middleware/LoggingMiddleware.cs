
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareDotNetCore.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LoggingOptions _options;

        public LoggingMiddleware(RequestDelegate next, IOptions<LoggingOptions> options)
        {
            _next = next;
            _options = options.Value;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs.log")
                .CreateLogger();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (_options.IsEnabled)
            {
                Log.Information($"{DateTime.Now}: Beginning request with {httpContext.Request.Path}");
            }
            await _next(httpContext);
            if (_options.IsEnabled)
            {
                Log.Information($"{DateTime.Now}: Completed request with {httpContext.Request.Path}");
            }
        }
    }

    public class LoggingOptions
    {
        public bool IsEnabled { get; set; }
    }
}
