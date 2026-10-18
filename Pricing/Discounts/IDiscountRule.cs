using BeeCafe.Domain;

namespace BeeCafe.Pricing.Discounts;

public interface IDiscountRule
{
    decimal Apply(Order order, decimal subtotal);
}
