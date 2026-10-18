using BeeCafe.Domain;
using BeeCafe.Notifications;
using BeeCafe.Persistence;
using BeeCafe.Pricing;
using BeeCafe.Pricing.Discounts;
using BeeCafe.Receipts;

namespace BeeCafe.Api;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", PlaceOrder).WithName("PlaceOrder");
        return app;
    }

    private static IResult PlaceOrder(PlaceOrderRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerEmail)) {
            return Results.BadRequest(new { error = "CustomerEmail is required." });
        }

        if (request.Items is null || request.Items.Count == 0) {
            return Results.BadRequest(new { error = "At least one item is required." });
        }

        var order = new Order(request.CustomerEmail, request.IsStudent, request.LoyaltyStamps);
        foreach (var name in request.Items)
        {
            if (!Menu.TryFind(name, out var item))
                return Results.BadRequest(new { error = $"Unknown item: {name}" });
            order.Add(item);
        }

        // Rules apply in this order, each one to the result of the previous
        var calculator = new PriceCalculator(
        [
            new StudentDiscount(),
            new HappyHourDiscount(TimeProvider.System),
            new EarlyBirdDiscount(TimeProvider.System),
            new LoyaltyDiscount()
        ]);
        var total = calculator.Total(order);
        var gst = calculator.Gst(total);

        new ReceiptPrinter().Print(order, total, gst);
        new OrderRepository().Save(order, total);
        new EmailNotifier().Send(order.CustomerContact, "your order is ready!");

        return Results.Ok(new PlaceOrderResponse(request.Items, total, gst));
    }
}
