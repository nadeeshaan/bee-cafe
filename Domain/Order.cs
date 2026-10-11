namespace BeeCafe.Domain;

public class Order(string customerContact, bool isStudent)
{
    private readonly List<MenuItem> _items = [];

    public string CustomerContact { get; } = customerContact;
    public bool IsStudent { get; } = isStudent;
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    public void Add(MenuItem item) => _items.Add(item);
}
