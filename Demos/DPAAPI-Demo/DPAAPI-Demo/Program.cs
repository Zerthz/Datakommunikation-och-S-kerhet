using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddDataProtection();
var services = serviceCollection.BuildServiceProvider();

var protectorProvider = services.GetRequiredService<IDataProtectionProvider>();
var protector = protectorProvider.CreateProtector("Example.Purpose");

var result = protector.Protect("Hej alla");

Console.WriteLine($"Encrypted value: {result}");

var decrypted = protector.Unprotect(result);
Console.WriteLine($"Decrypted valute: {decrypted}");

Console.ReadLine();