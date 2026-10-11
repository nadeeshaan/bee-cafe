using BeeCafe.Domain;

namespace BeeCafe.Receipts;

// Changes when the accountant changes what a receipt shows
public class ReceiptPrinter
{
    public void Print(Order order, decimal total, decimal gst)
    {
        Console.WriteLine("--- <> Cafe ---");
        foreach (var item in order.Items)
            Console.WriteLine($"  {item.Name,-12} ${item.Price:0.00}");
        Console.WriteLine($"Total: ${total:0.00}");
        Console.WriteLine($"Includes GST: ${gst:0.00}");
    }
}
