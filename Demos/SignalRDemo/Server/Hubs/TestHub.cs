using Microsoft.AspNetCore.SignalR;
using Server.Models;
using Server.State;

namespace Server.Hubs
{
    public class TestHub : Hub
    {
        private readonly ILogger<TestHub> _logger;
        private readonly IAppState _appState;
        private string _group = "MyGroup";

        // hubben är transient men appstate är singleton, så data där
        // sparas så länge som servern lever
        public TestHub(ILogger<TestHub> logger, IAppState appState)
        {
            _logger = logger;
            _appState = appState;
        }

        public Task<int> Multiply(int a, int b)
            => Task.FromResult(a * b);
        public async Task SelfLog(string message)
        {
            // Bara för att vi ska ha lite delay så att saker händer.
            await Task.Delay(250);
            // Hubben invokear en metod som heter log som finns i clientens context
            await Clients.All.SendAsync("Log", message);
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
        public async Task JoinGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _group);

        }


        // Todo SKAPA EN METOD FÖR ATT SKICKA TILL GRUPP
        public async Task MessageGroup(string message)
        {
            await Clients.Group(_group).SendAsync("Send", message);
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"New connection = {Context.ConnectionId}");

            
            return base.OnConnectedAsync();
        }

        public void Store(string value)
        {
            _appState.Store(Context.ConnectionId, value);
        }
        public string GetStoredValue()
        {
            return _appState.Get(Context.ConnectionId);
        }
    }
}
