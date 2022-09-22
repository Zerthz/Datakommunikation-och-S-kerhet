
// Vi vill ha 2 klienter portar 15005 - 15006
// client 1 skickar till 15006 och client 2 lyssnar

// Det här är lyssnaren
using System.Net;
using System.Net.Sockets;
using System.Text;

var endpoint = new IPEndPoint(IPAddress.Loopback, 15006);
using var client = new UdpClient(endpoint);

// Ta emot meddelande
Console.WriteLine($"Listening on port: {endpoint.Port}..");
var result = await client.ReceiveAsync();

// Skriv ut meddelande
Console.WriteLine(
    $"Recieved message " +
    $"\"{string.Join(",", Encoding.UTF8.GetString(result.Buffer))}\"" +
    $" from " +
    $"{result.RemoteEndPoint.Port}");

// Skapa svar
var message = "Thanks for your letter, reading now";

// Skicka svar
await client.SendAsync(Encoding.UTF8.GetBytes(message),
    result.RemoteEndPoint);

// Blocka
await client.ReceiveAsync();    