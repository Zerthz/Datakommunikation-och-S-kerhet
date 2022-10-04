using Microsoft.AspNetCore.SignalR.Client;

var jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImZlbGl4ZSIsInN1YiI6ImZlbGl4ZSIsImp0aSI6IjE0NzM5NjIwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTM0MzIiLCJodHRwczovL2xvY2FsaG9zdDo0NDM5NyIsImh0dHA6Ly9sb2NhbGhvc3Q6NTExNCIsImh0dHBzOi8vbG9jYWxob3N0OjcyMzYiXSwibmJmIjoxNjY0ODAwMDczLCJleHAiOjE2NzI3NDg4NzMsImlhdCI6MTY2NDgwMDA3NCwiaXNzIjoiZG90bmV0LXVzZXItand0cyJ9._f3IwTO7tR6xkXbO7CueFj5Qk2BBsw16esFLb0cIfVs";
var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7236/Test", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(jwt);
    })
    .WithAutomaticReconnect()
    .Build();

Console.WriteLine("wait..");
Console.ReadLine();
await connection.StartAsync();
Console.WriteLine("Connected!");
Console.WriteLine(await connection.InvokeAsync<int>("Restricted"));