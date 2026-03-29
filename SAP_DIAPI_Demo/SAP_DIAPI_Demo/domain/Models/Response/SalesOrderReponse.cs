using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Models.Response
{
    public class SalesOrderResponse
    {
      
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string CardCode { get; set; } = string.Empty;
        public string CardName { get; set; } = string.Empty;
        public double DocTotal { get; set; }
        public string Currency { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[DocEntry: {DocEntry}, DocNum: {DocNum}, CardCode: {CardCode}," +
                $" Total: {DocTotal}, CardName: {CardName}, DocTotal: {DocTotal}, Currency: {Currency}]";
        }
    }
}
