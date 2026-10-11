using BeeCafe.Domain;

namespace BeeCafe.Persistence;

// Changes when IT changes how orders are stored
public class OrderRepository
{
    // Simulated. Real code should use parameterised queries.
    public void Save(Order order, decimal total)
    {
        var items = string.Join(",", order.Items.Select(i => i.Name));
        Console.WriteLine($"INSERT INTO orders VALUES ('{items}', {total})");
    }
}
