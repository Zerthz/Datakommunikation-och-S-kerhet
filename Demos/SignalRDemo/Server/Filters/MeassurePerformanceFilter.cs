using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace Server.Filters
{
    public class MeassurePerformanceFilter : IHubFilter
    {
        private readonly ILogger<MeassurePerformanceFilter> _logger;

        public MeassurePerformanceFilter(ILogger<MeassurePerformanceFilter> logger)
        {
            _logger = logger;
        }

        public async ValueTask<object?> InvokeMethodAsync(
            HubInvocationContext invocationContext,
            Func<HubInvocationContext, ValueTask<object?>> next)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();


            var result = await next(invocationContext);

            stopwatch.Stop();
                
            _logger.LogInformation($"{invocationContext.HubMethodName} " +
                $"took {stopwatch.ElapsedMilliseconds} ms");
            
            return result;
        }

    }
}
