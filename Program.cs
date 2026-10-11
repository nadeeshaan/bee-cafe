using BeeCafe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "BeeCafe"));
}

app.UseHttpsRedirection();

app.MapPost("/orders", (PlaceOrderRequest request) =>
    {
        if (string.IsNullOrWhiteSpace(request.CustomerEmail))
            return Results.BadRequest(new { error = "CustomerEmail is required." });
        if (request.Items is null || request.Items.Count == 0)
            return Results.BadRequest(new { error = "At least one item is required." });

        var order = new Order(request.CustomerEmail, request.IsStudent);
        foreach (var item in request.Items) order.AddItem(item);

        decimal total;
        try
        {
            total = order.Total();
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        order.PrintReceipt();
        order.SaveToDatabase();
        order.EmailCustomer();

        return Results.Ok(new PlaceOrderResponse(request.Items, total));
    })
    .WithName("PlaceOrder");

app.Run();

record PlaceOrderRequest(string CustomerEmail, bool IsStudent, List<string> Items);

record PlaceOrderResponse(IReadOnlyList<string> Items, decimal Total);
