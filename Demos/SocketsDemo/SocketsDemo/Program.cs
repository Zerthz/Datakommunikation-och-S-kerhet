using System.Net;
using System.Net.Sockets;

var endpoint = new IPEndPoint(IPAddress.Loopback, 5005);

// socket är en logisk abstraktion av en port
using var socket = new Socket(
    endpoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp
    );

socket.Bind(endpoint);
socket.Listen();

Console.WriteLine("Listening..");

var handler = await socket.AcceptAsync();
while (true)
{

}

Console.ReadLine();