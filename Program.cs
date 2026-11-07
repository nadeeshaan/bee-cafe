using BeeCafe.Api;
using BeeCafe.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Singleton so saved orders are shared by every request until the app restarts
builder.Services.AddSingleton<InMemoryOrderStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "BeeCafe"));
}

app.UseHttpsRedirection();

app.MapOrderEndpoints();
app.MapKitchenEndpoints();

app.Run();
