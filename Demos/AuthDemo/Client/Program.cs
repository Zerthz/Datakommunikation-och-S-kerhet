using Microsoft.AspNetCore.SignalR.Client;

var jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkZlbGl4Iiwic3ViIjoiRmVsaXgiLCJqdGkiOiI1M2ViNDQ4MSIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjUzNDMyIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTciLCJodHRwOi8vbG9jYWxob3N0OjUxMTQiLCJodHRwczovL2xvY2FsaG9zdDo3MjM2Il0sIm5iZiI6MTY2NDkyMDg4NCwiZXhwIjoxNjcyODY5Njg0LCJpYXQiOjE2NjQ5MjA4ODUsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.r1PBMrmo-lEivCLKcHWJw112JcVxdSt1-Ctcm5YS_Y8";
var jwtFelix = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkZlbGl4Iiwic3ViIjoiRmVsaXgiLCJqdGkiOiJmZjQ4MTUzMCIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjUzNDMyIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTciLCJodHRwOi8vbG9jYWxob3N0OjUxMTQiLCJodHRwczovL2xvY2FsaG9zdDo3MjM2Il0sIm5iZiI6MTY2NDkyMTQ0MiwiZXhwIjoxNjcyODcwMjQyLCJpYXQiOjE2NjQ5MjE0NDMsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.XSNCBP2_k85HK7zvUtCEYG3Zz9j13666u0LzwkD_PZ4";
var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7236/Test", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(jwtFelix);
    })
    .WithAutomaticReconnect()
    .Build();

Console.WriteLine("wait..");
Console.ReadLine();
await connection.StartAsync();
Console.WriteLine("Connected!");
var foo = await connection.InvokeAsync<int>("Restricted");
Console.WriteLine(foo);
Console.ReadLine();