

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Uppgift2.Domain;

var endpoint = new IPEndPoint(IPAddress.Loopback, 15006);
using var client = new UdpClient(endpoint);

// Ta emot meddelande
Console.WriteLine($"Listening on port {endpoint.Port}");
var result = await client.ReceiveAsync();

// Få meddelande till skrivbart
var resJson = string.Join("", Encoding.UTF8.GetString(result.Buffer));
var resObj = JsonSerializer.Deserialize<DataClass>(resJson);

if (resObj == null)
    throw new NullReferenceException();

// Skriv ut meddelande
Console.WriteLine($"Message from: {result.RemoteEndPoint.Port}" +
                        $"\n\"{resObj.InitialMessage}\"" +
                        $"\nThis message has been read {resObj.NoTimesRead} time(s)");

// Skapa svar
resObj.ResponseMessage = "Nämen tjenaremors!";
resObj.NoTimesRead++;

var responseJson = JsonSerializer.Serialize(resObj);
byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);

// Skicka svar
await client.SendAsync(
    responseBytes,
    result.RemoteEndPoint);

// Blocka
await client.ReceiveAsync();