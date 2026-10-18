namespace BeeCafe.Api;

public record PlaceOrderRequest(string CustomerEmail, bool IsStudent, List<string> Items, int LoyaltyStamps = 0);

public record PlaceOrderResponse(IReadOnlyList<string> Items, decimal Total, decimal Gst);
