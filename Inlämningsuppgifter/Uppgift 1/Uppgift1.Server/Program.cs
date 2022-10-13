
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Uppgift1.Domain;

// Skapa en tcp server på socket 3003
var endpoint = new IPEndPoint(IPAddress.Loopback, 3003);
using var socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

socket.Bind(endpoint);
socket.Listen();
Console.WriteLine($"Listening on port: {endpoint.Port}..");

// ta emot en connection på port 3003
var handler = await socket.AcceptAsync(CancellationToken.None);
Console.WriteLine("Accepted connection");

while (true)
{
    // lyssna efter meddelande & ta emot ett
    var buffer = new byte[2048];
    var lengthRecieved = await handler.ReceiveAsync(buffer, SocketFlags.None);
    
    var data = buffer.Take(lengthRecieved).ToArray();

    // skapa upp en encoding som matchar clientens inget paket först
    // få tillbaka det serialiserade meddelandet till ett objekt
    var encodingNoBom = new UTF8Encoding(false);
    var responseBytes = encodingNoBom.GetString(data);
    var responseObj = JsonSerializer.Deserialize<DataClass>(responseBytes);

    // lägg till vårt meddelande från servern
    responseObj!.Message = "Adding a message from server";
    // skriv ut vad vi fick från klienten
    Console.WriteLine($"Recieved message from client:" +
                            $"\n    Name: {responseObj.Name}" +
                            $"\n    Age: {responseObj.Age}");

    // serialisera och skicka tillbaka
    var jsonResponse = JsonSerializer.Serialize(responseObj);
    var sendBytes = Encoding.UTF8.GetBytes(jsonResponse);
    await handler.SendAsync(sendBytes, SocketFlags.None);

    

   
}