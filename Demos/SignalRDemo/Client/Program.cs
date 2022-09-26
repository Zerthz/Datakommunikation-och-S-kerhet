
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5233/TestHub")
    .WithAutomaticReconnect()
    .Build();

Console.WriteLine("Väntar");
Console.ReadLine();

await connection.StartAsync();

Console.WriteLine("Connected!");