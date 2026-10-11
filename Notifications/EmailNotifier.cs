namespace BeeCafe.Notifications;

// Changes when marketing changes how customers are contacted
public class EmailNotifier
{
    public void Send(string to, string message) =>
        Console.WriteLine($"Emailing {to}: {message}");
}
