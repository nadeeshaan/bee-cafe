using BeeCafe.Domain;

namespace BeeCafe.Pricing.Discounts;

public class HappyHourDiscount(TimeProvider time) : IDiscountRule
{
    private static readonly TimeOnly Start = new(14, 0);
    private static readonly TimeOnly End = new(16, 0);

    public decimal Apply(Order order, decimal subtotal)
    {
        var now = TimeOnly.FromDateTime(time.GetLocalNow().DateTime);
        // IsBetween includes the start time and excludes the end time
        return now.IsBetween(Start, End) ? subtotal * 0.8m : subtotal;
    }
}
