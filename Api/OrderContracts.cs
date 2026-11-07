namespace BeeCafe.Api;

public record PlaceOrderRequest(
    string CustomerEmail,
    bool IsStudent,
    List<string> Items,
    string PaymentMethod,
    int LoyaltyStamps = 0);

public record PlaceOrderResponse(IReadOnlyList<string> Items, decimal Total, decimal Gst, string PaymentMethod);

public record RefundRequest(string PaymentMethod, decimal Amount);

public record RefundResponse(string PaymentMethod, decimal Amount);
