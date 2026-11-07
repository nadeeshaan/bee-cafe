using BeeCafe.Domain;

namespace BeeCafe.Persistence;

public interface IOrderLookup
{
    Order? GetById(Guid id);
}
