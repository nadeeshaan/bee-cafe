namespace BeeCafe.Payments;

// Anything a customer can pay with
public interface IPaymentMethod
{
    string Name { get; }
    void Pay(decimal amount);
}
