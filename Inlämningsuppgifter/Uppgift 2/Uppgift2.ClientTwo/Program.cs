

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Uppgift2.Domain;

var endpoint = new IPEndPoint(IPAddress.Loopback, 6006);
using var client = new UdpClient(endpoint);

Console.WriteLine($"Listening on port {endpoint.Port}");

var result = await client.ReceiveAsync();

var msgJson = string.Join(",", Encoding.UTF8.GetString(result.Buffer));

var msgObj = JsonSerializer.Deserialize<DataClass> (msgJson);
if(msgObj == null)
    throw new NullReferenceException();

Console.WriteLine($"Recieved this from {result.RemoteEndPoint.Port}" +
                        $"\n    \"{msgObj.InitialMessage}\"" +
                        $"\nThis message has been read {msgObj.NoTimesRead}times");

msgObj.NoTimesRead++;
msgObj.ResponseMessage = "Oho, trevligt att råkas!";

await client.ReceiveAsync();