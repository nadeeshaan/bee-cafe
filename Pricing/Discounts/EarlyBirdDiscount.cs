using BeeCafe.Domain;

namespace BeeCafe.Pricing.Discounts;

public class EarlyBirdDiscount(TimeProvider time) : IDiscountRule
{
    private static readonly TimeOnly Cutoff = new(8, 0);

    public decimal Apply(Order order, decimal subtotal)
    {
        var now = TimeOnly.FromDateTime(time.GetLocalNow().DateTime);
        return now < Cutoff ? Math.Max(0m, subtotal - 1.00m) : subtotal;
    }
}
