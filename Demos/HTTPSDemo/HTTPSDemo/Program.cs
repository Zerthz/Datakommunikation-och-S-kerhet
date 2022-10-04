using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// För att skapa ett https cert för dev purposes så kan man gå in i terminalen och dra
// dotnet dev-certs https --check
// sen dotnet dev-certs https --trust
// Så borde man få ett certifikat som man kan använda.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src https:");
    await next();
});

app.MapRazorPages();

app.Run();
