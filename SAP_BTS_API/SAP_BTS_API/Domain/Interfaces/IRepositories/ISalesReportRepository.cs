namespace SAP_BTS_API.Domain.Interfaces.IRepositories
{
    public interface ISalesReportRepository
    {
        string GetSalesFilterReportQuery(string fromDate, string toDate, string listCardCode);
    }
}
