

// Det här är skickaren
using System.Net;
using System.Net.Sockets;
using System.Text;

var endpoint = new IPEndPoint(IPAddress.Loopback, 15005);
using var client = new UdpClient(endpoint);



string message = "Hello World!";
byte[] bytes = Encoding.UTF8.GetBytes(message);

await client.SendAsync
    (bytes,
    new IPEndPoint(IPAddress.Loopback, 15006),CancellationToken.None);

Console.WriteLine($"Sending {message} to port 15006");
await client.ReceiveAsync();
