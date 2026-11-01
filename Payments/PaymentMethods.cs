using System.Diagnostics.CodeAnalysis;

namespace BeeCafe.Payments;

public static class PaymentMethods
{
    public static IReadOnlyList<IPaymentMethod> All { get; } = [new CardPaymentMode(), new GiftCard()];

    public static bool TryFind(string? name, out IPaymentMethod? method)
    {
        method = All.FirstOrDefault(m => m.Name == name);
        return method is not null;
    }
}
