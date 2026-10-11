using System.Diagnostics.CodeAnalysis;

namespace BeeCafe.Domain;

public record MenuItem(string Name, decimal Price);

public static class Menu
{
    public static readonly MenuItem Latte = new("latte", 5.50m);
    public static readonly MenuItem FlatWhite = new("flat white", 5.00m);
    public static readonly MenuItem LongBlack = new("long black", 4.50m);

    public static IReadOnlyList<MenuItem> All { get; } = [Latte, FlatWhite, LongBlack];

    public static bool TryFind(string name, [NotNullWhen(true)] out MenuItem? item)
    {
        item = All.FirstOrDefault(i => i.Name == name);
        return item is not null;
    }
}
