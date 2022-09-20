using System.Net;
using System.Net.Sockets;
using System.Text;

var endpoint = new IPEndPoint(IPAddress.Loopback, 5005);
using var socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

socket.Bind(endpoint);
socket.Listen();

Console.WriteLine("Listening on port " + endpoint.Port);

var handler = await socket.AcceptAsync(CancellationToken.None);

Console.WriteLine("Accepted connection");

while (true)
{
    var buffer = new byte[2048];
    var lengthReceived = await handler.ReceiveAsync(buffer, SocketFlags.None);
   
    var data = buffer.Take(lengthReceived).ToArray();

    // handler.SendAsync();
    var response = Encoding.UTF8.GetString(data);
    Console.WriteLine(response);

    var foo = await handler.SendAsync(data, SocketFlags.None);
    Console.WriteLine(foo);

   


}
