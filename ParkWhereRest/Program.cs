using ParkWhereLib;
using ParkWhereRest.Controllers;

var builder = WebApplication.CreateBuilder(args);

// --- Din CORS opsætning (Uændret) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

// --- Din HttpClient opsætning (Uændret) ---
builder.Services.AddHttpClient("MotorApi", client =>
{
    var motorApiConfig = builder.Configuration.GetSection("MotorApi");
    client.BaseAddress = new Uri(motorApiConfig["BaseUrl"]!);
    client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", motorApiConfig["ApiKey"]);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// ... resten af din fil er fin som den er ...
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