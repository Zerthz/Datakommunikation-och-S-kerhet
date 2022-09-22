

// Det här är skickaren
using System.Net;
using System.Net.Sockets;
using System.Text;

var endpoint = new IPEndPoint(IPAddress.Loopback, 15005);
using var client = new UdpClient(endpoint);


// Skapa Meddelande
string message = "Hello World!";
byte[] bytes = Encoding.UTF8.GetBytes(message);

// Skicka meddelande
Console.WriteLine($"Sending {message} to port 15006");
await client.SendAsync
    (bytes,
    new IPEndPoint(IPAddress.Loopback, 15006),
    CancellationToken.None);

// Ta emot svar
var result = await client.ReceiveAsync();

// Skriv svaret
Console.WriteLine(
    $"Recieved message " +
    $"\"{string.Join(",", Encoding.UTF8.GetString(result.Buffer))}\"" +
    $" from " +
    $"{result.RemoteEndPoint.Port}");

// Blocka
await client.ReceiveAsync();
