namespace BeeCafe.Payments;

public class CardPaymentMode : IPaymentMethod, IRefundable
{
    public string Name => "card";

    // Simulated
    public void Pay(decimal amount) =>
        Console.WriteLine($"Charging card ${amount:0.00}");

    // Simulated
    public void Refund(decimal amount) =>
        Console.WriteLine($"Refunding ${amount:0.00} to card");
}
