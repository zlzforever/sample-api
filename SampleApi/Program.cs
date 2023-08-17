using Dapr;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr(x =>
{
    x.UseGrpcEndpoint("http://localhost:3500");
    x.UseHttpEndpoint("http://localhost:50001");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Dapr configurations
app.UseCloudEvents();
app.MapSubscribeHandler();

app.MapPost("/subEvent", [Topic("pubsub", "MessageEvent")](ILogger<Program> logger, MessageEvent item) =>
{
    logger.LogInformation($"{item.MessageType}: {item.Message}");
    return Results.Ok();
});

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
//
// app.UseAuthorization();

app.MapControllers();

app.Run();

internal record MessageEvent(string MessageType, string Message);