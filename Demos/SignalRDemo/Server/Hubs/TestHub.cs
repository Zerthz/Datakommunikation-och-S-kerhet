﻿using Microsoft.AspNetCore.SignalR;
using Server.Models;

namespace Server.Hubs
{
    public class TestHub : Hub
    {
        private readonly ILogger<TestHub> _logger;
        private string _group = "MyGroup";

        public TestHub(ILogger<TestHub> logger)
        {
            _logger = logger;
        }

        public Task<int> Multiply(int a, int b)
            => Task.FromResult(a * b);
        public async Task SelfLog(string message)
        {
            // Bara för att vi ska ha lite delay så att saker händer.
            await Task.Delay(1000000);
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

    }
}