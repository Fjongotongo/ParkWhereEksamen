using Microsoft.EntityFrameworkCore;
using ParkWhereLib;
using ParkWhereLib.DbService;
using ParkWhereLib.Models;
using ParkWhereRest.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});


builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddHttpClient("MotorApi", client =>
{
    var motorApiConfig = builder.Configuration.GetSection("MotorApi");
    client.BaseAddress = new Uri(motorApiConfig["BaseUrl"]!);
    client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", motorApiConfig["ApiKey"]);
});

bool useSql = true;
if (useSql)
{
    builder.Services.AddScoped<IParkingLot, ParkingLotDb>();
}
else
{
    builder.Services.AddSingleton<IParkingLot, ParkingLot>();
}


builder.Services.AddScoped(typeof(GenericDbService<>)); 


builder.Services.AddScoped<ParkingLotDb>();
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