using BeeCafe.Domain;
using BeeCafe.Pricing.Discounts;

namespace BeeCafe.Pricing;

// Changes when the owner changes prices or discounts
public class PriceCalculator(IEnumerable<IDiscountRule> discountRules)
{
    public decimal Total(Order order)
    {
        var subtotal = order.Items.Sum(i => i.Price);
        
        foreach (var rule in discountRules) {
            subtotal = rule.Apply(order, subtotal);
        }

        return subtotal;
    }

    public decimal Gst(decimal total) => Math.Round(total * 3m / 23m, 2);
}
