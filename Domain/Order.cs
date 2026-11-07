using System.Collections.Generic;

namespace BeeCafe.Domain;

public class Order(string customerContact, bool isStudent, int loyaltyStamps = 0)
{
    private readonly List<MenuItem> _items = [];

    public Guid Id { get; } = Guid.NewGuid();
    public string CustomerContact { get; } = customerContact;
    public bool IsStudent { get; } = isStudent;
    public int LoyaltyStamps { get; } = loyaltyStamps;
    public bool IsOpen { get; private set; } = true;
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    public void Add(MenuItem item) => _items.Add(item);

    public void Close() => IsOpen = false;
}
