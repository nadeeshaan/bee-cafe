using BeeCafe.Domain;

namespace BeeCafe.Pricing.Discounts;

public class StudentDiscount : IDiscountRule
{
    public decimal Apply(Order order, decimal subtotal) =>
        order.IsStudent ? subtotal * 0.9m : subtotal;
}
