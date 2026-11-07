namespace BeeCafe.Reporting;

public interface ISalesReport
{
    decimal GetDailySales(DateOnly day);
    void ExportToCsv(DateOnly from, DateOnly to, string path);
}
