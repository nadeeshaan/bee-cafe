namespace BeeCafe;

public class Order(string customerEmail, bool isStudent)
{
    private readonly List<string> _items = [];
    private readonly string _customerEmail = customerEmail;
    private readonly bool _isStudent = isStudent;

    public void AddItem(string item) => _items.Add(item);

    // Pricing + discounts
    public decimal Total()
    {
        decimal subtotal = 0m;

        foreach (var item in _items)
        {
            subtotal += item switch
            {
                "latte" => 5.50m,
                "flat white" => 5.00m,
                "long black" => 4.50m,
                _ => throw new ArgumentException($"Unknown item: {item}")
            };
        }

        if (_isStudent)
        {
            subtotal *= 0.9m;
        }

        return subtotal;
    }

    // Presentation
    public void PrintReceipt()
    {
        Console.WriteLine("--- <> Cafe ---");
        foreach (var item in _items) Console.WriteLine($"  {item}");
        Console.WriteLine($"Total: ${Total():0.00}");
    }

    // Persistence (simulated). Real code should use parameterised queries,
    // never SQL built from strings.
    public void SaveToDatabase() =>
        Console.WriteLine($"INSERT INTO orders VALUES ('{string.Join(",", _items)}', {Total()})");

    // Notification (simulated)
    public void EmailCustomer() =>
        Console.WriteLine($"Emailing {_customerEmail}: your order is ready!");
}
