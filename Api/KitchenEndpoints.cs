using BeeCafe.Persistence;

namespace BeeCafe.Api;

public static class KitchenEndpoints
{
    public static IEndpointRouteBuilder MapKitchenEndpoints(this IEndpointRouteBuilder app)
    {
        var kitchen = app.MapGroup("/kitchen").WithTags("Kitchen");

        kitchen.MapGet("/orders", GetOpenOrders).WithName("GetOpenOrders");
        return app;
    }

    private static IResult GetOpenOrders(InMemoryOrderStore orderStore)
    {
        // The kitchen only reads open orders, so it depends on IOpenOrdersReader alone
        IOpenOrdersReader openOrders = new OrderRepository(orderStore, TimeProvider.System);

        var orders = openOrders.GetOpenOrders()
            .Select(o => new KitchenOrderResponse(o.Id, o.Items.Select(i => i.Name).ToList()))
            .ToList();

        return Results.Ok(orders);
    }
}
