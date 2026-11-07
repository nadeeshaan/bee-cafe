using BeeCafe.Domain;

namespace BeeCafe.Persistence;

public interface IOrderWriter
{
    void Save(Order order, decimal total);
}
