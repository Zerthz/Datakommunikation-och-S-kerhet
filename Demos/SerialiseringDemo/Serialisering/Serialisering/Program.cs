using Serialisering;
using System.Numerics;
using System.Text.Json;

var instance = new ComplexObject
{
    Name = "Felix",
    Age = 27
};

// sändaren
var json = JsonSerializer.Serialize(instance);
Console.WriteLine($"JSON = {json}");

// mottagaren
var deserialize = JsonSerializer.Deserialize<ComplexObject>(json);
Console.WriteLine($"Deserialized.name={deserialize.Name}\nDeserialized.age={deserialize.Age}");
Console.ReadLine();