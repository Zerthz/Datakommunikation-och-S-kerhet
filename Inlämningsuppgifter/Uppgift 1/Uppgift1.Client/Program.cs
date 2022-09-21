
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Uppgift1.Domain;

using var tcpClient = new TcpClient();
await tcpClient.ConnectAsync("localhost", 69);

using var stream = tcpClient.GetStream();

var reader = new StreamReader(stream, new UTF8Encoding(false));
var writer = new StreamWriter(stream, new UTF8Encoding(false));


var felixObj = new DataClass { Name = "Felix", Age = 27 };
var jsonFelix = JsonSerializer.Serialize(felixObj);


await writer.WriteAsync(jsonFelix);
await writer.FlushAsync();


while(true)
{
    char[] buffer = new char[2048];
    
    var recievedLength = await reader.ReadAsync(buffer, CancellationToken.None);
    var jsonData = string.Join("", buffer.Take(recievedLength));

    var msg = JsonSerializer.Deserialize<DataClass>(jsonData);

    if (msg == null)
        throw new NullReferenceException();

    Console.WriteLine($"Message recieved from server:" +
                            $"\n    Name: {msg.Name}" +
                            $"\n    Age: {msg.Age}" +
                            $"\n    Message: {msg.Message}");


}

