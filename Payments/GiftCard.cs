namespace BeeCafe.Payments;

// Gift cards can pay but can't be refunded, so this does not implement IRefundable
public class GiftCard : IPaymentMethod
{
    public string Name => "giftCard";

    // Simulated
    // In the actual implementation, we check the database for available balance and do a verification as well
    public void Pay(decimal amount) =>
        Console.WriteLine($"Redeeming ${amount:0.00} from gift card");
}
