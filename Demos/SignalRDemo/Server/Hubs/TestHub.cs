using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class ComplexObject
    {
        public string Name { get; set; }
        public int Age{ get; set; }
    }
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
        public ComplexObject Complex(ComplexObject co)
        {
            return new ComplexObject
            {
                Name = co.Name,
                Age = co.Age * 2
            };
        }

        // Todo SKAPA EN METOD FÖR ATT LÄGGA TILL I GRUPP

        // Todo SKAPA EN METOD FÖR ATT SKICKA TILL GRUPP


        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"New connection = {Context.ConnectionId}");

            
            return base.OnConnectedAsync();
        }

    }
}
