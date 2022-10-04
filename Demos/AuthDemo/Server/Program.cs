using Server.Auth;
using Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyAllowFelix", builder =>
    {
        builder.AddRequirements(new OnlyAllowNameRequirement("felix"));
    });
});
builder.Services.AddAuthentication("Bearer").AddJwtBearer();


builder.Services.AddSignalR();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();
app.MapHub<TestHub>("/Test");
app.Run();
