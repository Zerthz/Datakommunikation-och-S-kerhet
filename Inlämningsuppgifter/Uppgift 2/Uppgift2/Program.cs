
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Uppgift2.Domain;

var endpoint = new IPEndPoint(IPAddress.Loopback, 3003);
using var client = new UdpClient(endpoint);

DataClass msgObj = new DataClass { NoTimesRead = 1, InitialMessage = "Hello There Friend" };
var msgJson = JsonSerializer.Serialize(msgObj);
byte[] bytes = Encoding.UTF8.GetBytes(msgJson);

Console.WriteLine("Sending a message over to port 6006");
await client.SendAsync(bytes, new IPEndPoint(IPAddress.Loopback, 6006), CancellationToken.None);

var response = await client.ReceiveAsync();

