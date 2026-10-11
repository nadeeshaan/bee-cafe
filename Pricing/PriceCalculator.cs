using BeeCafe.Domain;

namespace BeeCafe.Pricing;

// Changes when the owner changes prices or discounts
public class PriceCalculator
{
    public decimal Total(Order order)
    {
        var subtotal = order.Items.Sum(i => i.Price);

        if (order.IsStudent)
        {
            subtotal *= 0.9m;
        }

        return subtotal;
    }

    public decimal Gst(decimal total) => Math.Round(total * 3m / 23m, 2);
}
