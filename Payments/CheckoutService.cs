namespace BeeCafe.Payments;

// Changes when the cafe changes how it takes payment
public class CheckoutService
{
    // Any IPaymentMethod can stand in here
    public void Checkout(IPaymentMethod paymentMethod, decimal total) =>
        paymentMethod.Pay(total);
}
