using BeeCafe.Domain;
using BeeCafe.Reporting;

namespace BeeCafe.Persistence;

// Changes when IT changes how orders are stored. One class implements every
// small interface, but each caller depends only on the interface it needs.
public class OrderRepository(InMemoryOrderStore store, TimeProvider time) :
    IOrderWriter, IOrderLookup, IOpenOrdersReader, ISalesReport, IOrderCleanup, ISalesOverviews
{
    public void Save(Order order, decimal total) =>
        store.Add(new StoredOrder(order, total, time.GetLocalNow()));

    public Order? GetById(Guid id) => store.Find(id)?.Order;

    public IReadOnlyList<Order> GetOpenOrders() =>
        store.All
            .Where(o => o.Order.IsOpen)
            .OrderBy(o => o.PlacedAt)
            .Select(o => o.Order)
            .ToList();

    public decimal GetDailySales(DateOnly day) =>
        store.All.Where(o => o.PlacedOn == day).Sum(o => o.Total);

    public void ExportToCsv(DateOnly from, DateOnly to, string path)
    {
        var rows = store.All
            .Where(o => o.PlacedOn >= from && o.PlacedOn <= to)
            .OrderBy(o => o.PlacedAt)
            .Select(o => string.Join(",",
                o.Order.Id,
                o.PlacedAt.ToString("O"),
                Csv(string.Join(";", o.Order.Items.Select(i => i.Name))),
                o.Total));

        File.WriteAllLines(path, ["id,placed_at,items,total", .. rows]);
    }

    public void DeleteOrdersOlderThan(DateOnly cutoff)
    {
        foreach (var order in store.All.Where(o => o.PlacedOn < cutoff).ToList())
            store.Remove(order.Order.Id);
    }

    // Most ordered items first; ties are broken by item name so the order is stable
    public IReadOnlyList<BestSeller> GetBestSellers(int count) =>
        store.All
            .SelectMany(o => o.Order.Items)
            .GroupBy(i => i.Name)
            .Select(g => new BestSeller(g.Key, g.Count()))
            .OrderByDescending(b => b.QuantitySold)
            .ThenBy(b => b.Item)
            .Take(count)
            .ToList();

    private static string Csv(string value) => $"\"{value.Replace("\"", "\"\"")}\"";
}
