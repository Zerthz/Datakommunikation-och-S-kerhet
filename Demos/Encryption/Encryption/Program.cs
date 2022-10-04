using System.Security.Cryptography;
using System.Text;
Console.WriteLine("Starting");

byte[] keyBytes =
{
    0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80,
    0x80, 0x30, 0x70, 0x60, 0x40, 0x30, 0x10, 0x20
};

using var data = new MemoryStream(2048);
var iv = await Encrypt(keyBytes, "Hej alla", data);

var encryptedValue = data.ToArray();
Console.WriteLine(
    $"Encrypted value as string " +
    $"{Encoding.UTF8.GetString(encryptedValue)}"
    );

var decrypt = await Decrypt(keyBytes, iv, encryptedValue);
Console.WriteLine($"Decrypted value as string = {decrypt}");

Console.ReadLine();


async Task<byte[]> Encrypt(byte[] key, string message, Stream output)
{
    using var aesEncrypt = Aes.Create();
    aesEncrypt.Key = key;

    var iv = aesEncrypt.IV;

    using var cryptoStream = 
        new CryptoStream(
            output,
            aesEncrypt.CreateEncryptor(),
            CryptoStreamMode.Write);

    using var streamWriter = new StreamWriter(cryptoStream);

    await streamWriter.WriteAsync(message);
    return iv;
}

async Task<string> Decrypt(byte[] key, byte[] iv, byte[] message)
{
    using var aesDecrypt = Aes.Create();
    using var input = new MemoryStream(message);
    using var cryptoStream = 
        new CryptoStream(
            input, 
            aesDecrypt.CreateDecryptor(key, iv),
            CryptoStreamMode.Read
        );

    using var reader = new StreamReader(cryptoStream);

    var result = await reader.ReadToEndAsync();

    return result;
}