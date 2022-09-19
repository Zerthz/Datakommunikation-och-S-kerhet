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
    // allokerar en array men ger den ingen data
    var buffer = new byte[2048];

    // när den får data så fyller den vår buffer array med detta, och det är bara det som ligger i 
    // length recieved som är data
    var lengthRecieved = await handler.ReceiveAsync(buffer, SocketFlags.None);
    
    var data = buffer.Take(lengthRecieved).ToArray();

    Console.WriteLine($"Data = {string.Join(", ", data)}");


}

Console.ReadLine();