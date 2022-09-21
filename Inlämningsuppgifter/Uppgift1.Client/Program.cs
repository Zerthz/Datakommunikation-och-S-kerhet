
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Uppgift1.Domain;

using var tcpClient = new TcpClient();
await tcpClient.ConnectAsync("localhost", 69);

using var stream = tcpClient.GetStream();

//var reader = new StreamReader(stream, Encoding.UTF8);
//var writer = new StreamWriter(stream, Encoding.UTF8);


var felixObj = new DataClass { Name = "Felix", Age = 27 };
var jsonFelix = JsonSerializer.Serialize(felixObj);
var jsonFelixBytes = Encoding.UTF8.GetBytes(jsonFelix);
await stream.WriteAsync(jsonFelixBytes,CancellationToken.None);

//await writer.WriteAsync(jsonFelix);
//await writer.FlushAsync();



while(true)
{
    byte[] buffer = new byte[1024];
    int bytesRecieved = stream.Read(buffer);
    string jsonData = Encoding.UTF8.GetString(buffer.AsSpan(0, bytesRecieved));
    var deserData = JsonSerializer.Deserialize<DataClass>(jsonData);

    if (deserData == null)
        throw new NullReferenceException();

    Console.WriteLine($"Message recieved from server:" +
                            $"\n    Name: {deserData.Name}" +
                            $"\n    Age: {deserData.Age}" +
                            $"\n    Message: {deserData.Message}");


}

