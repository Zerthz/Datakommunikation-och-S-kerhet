
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5233/TestHub")
    .WithAutomaticReconnect()
    .Build();

Console.WriteLine("Väntar");
Console.ReadLine();

await connection.StartAsync();
connection.Closed += async reason =>
{
    Console.WriteLine(reason + " hände");
};
connection.Reconnecting += async status =>
{
    Console.WriteLine(status);
};
connection.Reconnected += async status =>
{
    Console.WriteLine("");
};



Console.WriteLine("Connected!");

Console.ReadLine();
