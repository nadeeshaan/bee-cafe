namespace BeeCafe.Api;

public record PlaceOrderRequest(string CustomerEmail, bool IsStudent, List<string> Items);

public record PlaceOrderResponse(IReadOnlyList<string> Items, decimal Total, decimal Gst);
