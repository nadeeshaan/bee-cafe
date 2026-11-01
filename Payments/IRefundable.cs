namespace BeeCafe.Payments;

// Only methods that can actually give money back implement this, so no
// implementation of IPaymentMethod is forced to throw from a Refund it can't honour
public interface IRefundable
{
    void Refund(decimal amount);
}
