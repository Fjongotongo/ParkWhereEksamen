using ParkWhereLib;
using ParkWhereLib.Interfaces;
using ParkWhereRest.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddHttpClient("MotorApi", client => // Changed to a named client, her navngiver vi clienten "MotorApi", de får informationen fra appsettings.json
{
    var motorApiConfig = builder.Configuration.GetSection("MotorApi");
    client.BaseAddress = new Uri(motorApiConfig["BaseUrl"]!); 
    client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", motorApiConfig["ApiKey"]);
});




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ICarRepo, CarRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();