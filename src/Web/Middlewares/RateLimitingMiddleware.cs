using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Web.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        private const int LIMIT_GET = 40; 
        private const int LIMIT_POST = 5; 
        private readonly TimeSpan TIME_WINDOW = TimeSpan.FromMinutes(1);

        public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _cache = cache;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(ipAddress))
            {
                await _next(context);
                return;
            }

            var cacheKey = $"RateLimit_{ipAddress}";

            var requestInfo = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TIME_WINDOW;
                return new RequestCounter();
            });



            if (requestInfo != null)
            {
                requestInfo.Count++;

                if (UserIsNotAdminAndReachLimits(context, requestInfo))
                {
                    _logger.LogWarning("Rate limit aşıldı: {IpAddress}", ipAddress);
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    return;
                }
            }

            await _next(context);
        }

        private class RequestCounter
        {
            public int Count { get; set; } = 0;
        }

        private static bool UserIsNotAdminAndReachLimits(HttpContext context, RequestCounter requestInfo)
        {
            var isNotAuthenticated = context.User.Identity?.IsAuthenticated == false;
            var isPost = context.Request.Method == "POST";
            var isGet = context.Request.Method == "GET";

            if (!isNotAuthenticated)
                return false; 

            if (isPost && requestInfo.Count > LIMIT_POST)
                return true;

            return isGet && requestInfo.Count > LIMIT_GET;
        }

    }

}
