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
        
    }
}
