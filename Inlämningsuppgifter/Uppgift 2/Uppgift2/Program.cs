
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Uppgift2.Domain;

var endpoint = new IPEndPoint(IPAddress.Loopback, 15005);
using var client = new UdpClient(endpoint);

// Skapa meddelande 
DataClass msgObj = new DataClass { InitialMessage = "Nej men tjenare!" };
var msgJson = JsonSerializer.Serialize(msgObj);
byte[] bytes = Encoding.UTF8.GetBytes(msgJson);


// Skicka meddelande
Console.WriteLine("Sending a message over to port 15006");
await client.SendAsync(
    bytes,
    new IPEndPoint(IPAddress.Loopback, 15006),
    CancellationToken.None);

// Ta emot ett svar
var result = await client.ReceiveAsync();

// Få svaret till objekt igen
var resJson = string.Join(",", Encoding.UTF8.GetString(result.Buffer));
var resObj = JsonSerializer.Deserialize<DataClass>(resJson);

if (resObj == null)
    throw new NullReferenceException();

// Skriv svaret
Console.WriteLine($"Message from: {result.RemoteEndPoint.Port}" +
                        $"\n\"{resObj.ResponseMessage}\"" +
                        $"\nThis message has been read {resObj.NoTimesRead} time(s)");

// Blocka
await client.ReceiveAsync();
