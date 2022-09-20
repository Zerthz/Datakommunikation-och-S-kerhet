
// Vi vill ha 2 klienter portar 15005 - 15006
// client 1 skickar till 15006 och client 2 lyssnar

// Det här är lyssnaren
using System.Net;
using System.Net.Sockets;
using System.Text;

var endpoint = new IPEndPoint(IPAddress.Loopback, 15006);
using var client = new UdpClient(endpoint);

    Console.WriteLine($"Listening on port: {endpoint.Port}..");

    var result = await client.ReceiveAsync();
    Console.WriteLine(
        $"Recieved message " +
        $"\"{string.Join(",", Encoding.UTF8.GetString(result.Buffer))}\"" +
        $" from " +
        $"{result.RemoteEndPoint.Port}");
   