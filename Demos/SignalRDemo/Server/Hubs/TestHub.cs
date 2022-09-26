using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class TestHub : Hub
    {
        private readonly ILogger<TestHub> _logger;

        public TestHub(ILogger<TestHub> logger)
        {
            _logger = logger;
        }

        public Task<int> Multiply(int a, int b)
            => Task.FromResult(a * b);
        public void SelfLog(string message)
        {
            // Hubben invokear en metod som heter log som finns i clientens context
            Clients.All.SendAsync("Log", message);
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"New connection = {Context.ConnectionId}");

            
            return base.OnConnectedAsync();
        }

    }
}
