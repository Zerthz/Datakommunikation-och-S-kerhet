
using System.Text;

// olika abstraktionsnivåer. Streams är rätt abstrakta och svåra att jobbiga med
using var inputStream = File.OpenRead("Example.txt");
// nästa lättare nivå är readers, de tar strömmen och gör de lättare att jobba med
using var streamReader = new StreamReader(inputStream, Encoding.UTF8);

while (!streamReader.EndOfStream)
{
    var line = await streamReader.ReadLineAsync();
    Console.WriteLine(line);
}

// den här börjar i slutet på strömmen, eller vi är nu i slutet rättare sagt
Console.WriteLine(inputStream.Position);
// här går vi tillbaka till början
inputStream.Seek(0, SeekOrigin.Begin);
Console.WriteLine(inputStream.Position);

var outputStream = File.OpenWrite($"{Guid.NewGuid():N}.txt");
while (inputStream.CanRead)
{
    // Går det att läsa ett byte? ja läs det och gå ett byte frammåt, ökar position med 1
    var value = inputStream.ReadByte();

    // läs readbyte doc ovanför
    if (value == -1)
        break;

    outputStream.WriteByte((byte)value);
}

outputStream.Close();
outputStream.Dispose();


Console.ReadLine();