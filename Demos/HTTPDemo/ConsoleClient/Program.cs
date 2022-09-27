//Console.ReadLine();

var client = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5263"),
    DefaultRequestHeaders =
    {
        { "accept", "application/json" }
    }

};

var retry = true;
string output = string.Empty;
do
{
    Console.WriteLine("Calling api");
    var result = await CallApi(client);
    retry = !result.Item1;
    if (!retry)
        output = result.Item2;
    else
        await Task.Delay(2000);
} while (retry);

Console.WriteLine(output);



//var content = await response.Content.ReadAsStringAsync();

//Console.WriteLine(content);

async Task<(bool, string)> CallApi(HttpClient client)
{

	try
	{
        var response = await client.GetAsync("weatherforecast", CancellationToken.None);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return (true, content);

    }
    catch (Exception ex)
	{
        Console.WriteLine($"Error: {ex.Message}");
		return (false, null)!;
	}
}
