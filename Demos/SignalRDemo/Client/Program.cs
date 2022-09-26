
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5233/TestHub")
    .WithAutomaticReconnect()
    .Build();

Console.WriteLine("Väntar");
Console.ReadLine();

await connection.StartAsync();

//connection.Closed += async reason =>
//{
//    Console.WriteLine(reason + " hände");
//};
//connection.Reconnecting += async status =>
//{
//    Console.WriteLine(status);
//};
//connection.Reconnected += async status =>
//{
//    Console.WriteLine("");
//};



Console.WriteLine("Connected!");

// invoke måste man köra om metoden returenrar ngt
// om metoden inte returnerar ngt kan man köra sendasync
// query v command
var result = await connection.InvokeAsync<int>("Multiply", 2, 3);
Console.WriteLine("2 * 3 = " + result);

Console.ReadLine();
