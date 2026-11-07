namespace BeeCafe.Api;

public record KitchenOrderResponse(Guid Id, IReadOnlyList<string> Items);
