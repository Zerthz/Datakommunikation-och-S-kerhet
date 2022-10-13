
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Uppgift1.Domain;

// etablera connection
using var tcpClient = new TcpClient();
await tcpClient.ConnectAsync("localhost", 3003);

using var stream = tcpClient.GetStream();

// Skapa läsare & skrivare
// stäng av utf8 identifier. så att den inte skickar ett tomt paket först som trasslar till allt. 
var reader = new StreamReader(stream, new UTF8Encoding(false));
var writer = new StreamWriter(stream, new UTF8Encoding(false));


// skapa meddelande, message lämnas tomt och är där server skriver till os
var felixObj = new DataClass { Name = "Felix", Age = 27 };
var jsonFelix = JsonSerializer.Serialize(felixObj);

// skicka meddelande
await writer.WriteAsync(jsonFelix);
await writer.FlushAsync();


while(true)
{
    // Ta emot svaret.
    char[] buffer = new char[2048];    
    var recievedLength = await reader.ReadAsync(buffer, CancellationToken.None);
    var jsonData = string.Join("", buffer.Take(recievedLength));

    // få svaret till objekt igen
    var msg = JsonSerializer.Deserialize<DataClass>(jsonData);

    // validera att vi fick ut något
    if (msg == null)
        throw new NullReferenceException();

    // Printa svaret
    Console.WriteLine($"Message recieved from server:" +
                            $"\n    Name: {msg.Name}" +
                            $"\n    Age: {msg.Age}" +
                            $"\n    Message: {msg.Message}");
}

