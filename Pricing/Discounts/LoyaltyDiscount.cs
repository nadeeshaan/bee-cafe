using BeeCafe.Domain;

namespace BeeCafe.Pricing.Discounts;

public class LoyaltyDiscount : IDiscountRule
{
    private const int StampsForFreeCoffee = 9;

    public decimal Apply(Order order, decimal subtotal)
    {
        if (order.LoyaltyStamps < StampsForFreeCoffee || order.Items.Count == 0)
            return subtotal;
        var freeItem = order.Items.Min(i => i.Price);
        return Math.Max(0m, subtotal - freeItem);
    }
}
