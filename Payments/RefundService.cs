namespace BeeCafe.Payments;

public record RefundResult(bool Succeeded, string? Error = null)
{
    public static RefundResult Refunded() => new(true);
    public static RefundResult Rejected(string error) => new(false, error);
}

// Changes when the cafe changes how it gives money back
public class RefundService
{
    public RefundResult Refund(string paymentMethodName, decimal amount)
    {
        if (amount <= 0)
            return RefundResult.Rejected("Amount must be greater than zero.");

        if (!PaymentMethods.TryFind(paymentMethodName, out var paymentMethod))
            return RefundResult.Rejected($"Unknown payment method: {paymentMethodName}");

        // Only IRefundable methods are refunded, so GiftCard never needs a Refund that throws
        if (paymentMethod is not IRefundable refundable)
            return RefundResult.Rejected($"Payment method {paymentMethod.Name} does not support refunds.");

        refundable.Refund(amount);
        return RefundResult.Refunded();
    }
}
