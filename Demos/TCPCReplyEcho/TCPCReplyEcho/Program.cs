using System.Net.Sockets;
using System.Text;

using var tcpClient = new TcpClient();
await tcpClient.ConnectAsync("localhost", 5005);

using var stream = tcpClient.GetStream();

var reader = new StreamReader(stream, Encoding.UTF8);
var writer = new StreamWriter(stream, Encoding.UTF8);

var input = Console.ReadLine();
// writer.WriteAsync
await writer.WriteAsync(input);
// writer.FlushAsync
await writer.FlushAsync();

while (true) { 
    byte[] receiveBuffer = new byte[1024];
    int bytesReceived = stream.Read(receiveBuffer);
    string data = Encoding.UTF8.GetString(receiveBuffer.AsSpan(0, bytesReceived));

    Console.WriteLine($"Echo: {data}");
    Console.ReadLine();
}

Console.WriteLine("Broken");