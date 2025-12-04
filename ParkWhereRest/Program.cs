using ParkWhereLib;
using ParkWhereRest.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

builder.Services.AddHttpClient("MotorApi", client =>
{
    var motorApiConfig = builder.Configuration.GetSection("MotorApi");
    client.BaseAddress = new Uri(motorApiConfig["BaseUrl"]!);
    client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", motorApiConfig["ApiKey"]);
});

builder.Services.AddSingleton<ParkingLot>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

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