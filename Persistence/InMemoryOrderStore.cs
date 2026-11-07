using System.Collections.Concurrent;
using BeeCafe.Domain;

namespace BeeCafe.Persistence;

public record StoredOrder(Order Order, decimal Total, DateTimeOffset PlacedAt)
{
    public DateOnly PlacedOn => DateOnly.FromDateTime(PlacedAt.DateTime);
}

// Holds orders in memory for the life of the app; everything is lost on restart.
// Safe to share between requests.
public class InMemoryOrderStore
{
    private readonly ConcurrentDictionary<Guid, StoredOrder> _orders = new();

    public IEnumerable<StoredOrder> All => _orders.Values;

    public void Add(StoredOrder order) => _orders[order.Order.Id] = order;

    public StoredOrder? Find(Guid id) => _orders.GetValueOrDefault(id);

    public void Remove(Guid id) => _orders.TryRemove(id, out _);
}
