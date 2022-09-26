using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class TestHub : Hub
    {
        public TestHub()
        {                
        }

        public Task<int> Multiply(int a, int b)
            => Task.FromResult(a * b);
        public void SelfLog(string message)
        {
            // Hubben invokear en metod som heter log som finns i clientens context
            Clients.All.SendAsync("Log", message);
        }
    }
}
