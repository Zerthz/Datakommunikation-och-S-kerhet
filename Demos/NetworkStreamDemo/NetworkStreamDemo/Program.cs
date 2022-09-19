
using System.Net.Sockets;
using System.Text;

using var tcpClient = new TcpClient();
await tcpClient.ConnectAsync("localhost", 5005);

using var stream = tcpClient.GetStream();

var text = Console.ReadLine();
var buffer = Encoding.UTF8.GetBytes(text);
await stream.WriteAsync(buffer,0, buffer.Length, CancellationToken.None);

var result = new byte[1024];
int i = 0;

while (stream.CanRead)
{
    var value = stream.ReadByte();
    if (value == -1)
        break;

    result[i] = (byte)value;
    i++;
}

var response = Encoding.UTF8.GetString(result.Take(i + 1).ToArray());

Console.WriteLine(response);
Console.ReadLine();