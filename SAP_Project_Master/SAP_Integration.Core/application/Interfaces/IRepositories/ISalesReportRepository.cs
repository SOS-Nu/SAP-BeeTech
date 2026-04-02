using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Integration.Core.application.Interfaces.IRepositories
{
    public interface ISalesReportRepository
    {
        string GetSalesFilterReportQuery(string fromDate, string toDate, string listCardCode);
    }
}
