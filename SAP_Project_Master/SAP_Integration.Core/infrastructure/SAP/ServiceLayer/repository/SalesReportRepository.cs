using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP_Integration.Core.application.Interfaces.IRepositories;

namespace SAP_Integration.Core.infrastructure.repository
{
    public class SalesReportRepository : ISalesReportRepository
    {
        public string GetSalesFilterReportQuery(string fromDate, string toDate, string listCardCode)
        {
            // Logic xử lý format tham số như mình đã bàn ở trên
            string fDate = string.IsNullOrEmpty(fromDate) ? "NULL" : $"'{fromDate}'";
            string tDate = string.IsNullOrEmpty(toDate) ? "NULL" : $"'{toDate}'";
            string cardCodes = string.IsNullOrEmpty(listCardCode) ? "''" : $"'{listCardCode}'";

            return $"CALL \"USP_GetSalesFilterReport\"({fDate}, {tDate}, {cardCodes})";
        }
    }
}