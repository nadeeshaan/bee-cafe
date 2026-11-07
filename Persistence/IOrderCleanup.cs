namespace BeeCafe.Persistence;

public interface IOrderCleanup
{
    void DeleteOrdersOlderThan(DateOnly cutoff);
}
