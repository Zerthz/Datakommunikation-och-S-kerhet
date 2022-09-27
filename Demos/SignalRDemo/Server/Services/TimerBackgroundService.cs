using Microsoft.AspNetCore.SignalR;
using Server.Hubs;

namespace Server.Services
{
    // De här kan heta vad de vill behöver inte följa Service/*Service
    // Så inte som controllers osv

    // Backgroundservice är en IHostedService med bestpractices tillagt
    public class TimerBackgroundService : BackgroundService
    {
        private readonly ILogger<TimerBackgroundService> _logger;
        // En referens till en specifik hub
        private readonly IHubContext<TestHub> _hubContext;

        public TimerBackgroundService(
            ILogger<TimerBackgroundService> logger, 
            IHubContext<TestHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background Service Starting");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Notifying all clients");
                await _hubContext.Clients.All.SendAsync(
                    "Log",
                    DateTime.UtcNow.ToString("T"),
                    stoppingToken);

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
