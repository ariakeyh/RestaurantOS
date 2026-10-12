using System.Threading.RateLimiting;

namespace Api.Helpers;

public static class RateLimitHelper
{
    internal const string PolicyName = "fixed";

    public static void AddRateLimits(this IServiceCollection services, IWebHostEnvironment env)
    {
        if (env.IsProduction())
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? "anonymous",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            QueueLimit = 5,
                            PermitLimit = 15,
                            Window = TimeSpan.FromSeconds(10),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        }));
            });
        }
    }

    public class RateLimitConfig
    {
        public int QueueLimit { get; set; }
        public int PermitLimit { get; set; }
        public int WindowInSeconds { get; set; }
        public QueueProcessingOrder QueueProcessingOrder { get; set; }
    }
}