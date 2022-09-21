
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Uppgift1.Domain;


var endpoint = new IPEndPoint(IPAddress.Loopback, 69);
using var socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

socket.Bind(endpoint);
socket.Listen();
Console.WriteLine($"Listening on port: {endpoint.Port}..");

var handler = await socket.AcceptAsync(CancellationToken.None);
Console.WriteLine("Accepted connection");

while (true)
{
    var buffer = new byte[2048];
    var buffersize =  handler.ReceiveBufferSize;
    var lengthRecieved = await handler.ReceiveAsync(buffer, SocketFlags.None);
    
    var data = buffer.Take(lengthRecieved).ToArray();
    var responseBytes = Encoding.UTF8.GetString(data);
    Console.WriteLine(responseBytes);
    if(responseBytes != "")
    {
        var responseObj = JsonSerializer.Deserialize<DataClass>(responseBytes);
        responseObj!.Message = "Adding a message from server";
        Console.WriteLine($"Recieved message from client:" +
                                $"\n    Name: {responseObj.Name}" +
                                $"\n    Age: {responseObj.Age}");

        var jsonResponse = JsonSerializer.Serialize(responseObj);
        var sendBytes = Encoding.UTF8.GetBytes(jsonResponse);
        await handler.SendAsync(sendBytes, SocketFlags.None);

    }

   
}