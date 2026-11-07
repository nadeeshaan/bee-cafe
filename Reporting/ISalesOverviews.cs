namespace BeeCafe.Reporting;

public record BestSeller(string Item, int QuantitySold);

public interface ISalesOverviews
{
    IReadOnlyList<BestSeller> GetBestSellers(int count);
}
