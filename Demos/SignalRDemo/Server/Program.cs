using Microsoft.AspNetCore.SignalR;
using Server.Filters;
using Server.Hubs;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR(option =>
{
    option.AddFilter<MeassurePerformanceFilter>();
});
builder.Services.AddCors();
builder.Services.AddHostedService<TimerBackgroundService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:5104")
    .AllowAnyHeader()
    .WithMethods("GET", "POST")
    .AllowCredentials();
});

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<TestHub>("/TestHub");


app.Run();
