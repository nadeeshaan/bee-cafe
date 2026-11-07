using BeeCafe.Domain;

namespace BeeCafe.Persistence;

public interface IOpenOrdersReader
{
    IReadOnlyList<Order> GetOpenOrders();
}
