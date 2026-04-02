using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_BTS_API.Applications.DTOs.responses
{
    public class SalesOrderResponseDTO
    {
      
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string CardCode { get; set; } = string.Empty;
        public string CardName { get; set; } = string.Empty;
        public double DocTotal { get; set; }
        public List<SalesOrderLineResponseDTO> DocumentLines { get; set; } = new();
        public class SalesOrderLineResponseDTO
        {
            public int LineNum { get; set; }
            public string ItemCode { get; set; } = string.Empty;
            public string ItemDescription { get; set; } = string.Empty;
            public double Quantity { get; set; }
            public double Price { get; set; }
            public string Currency { get; set; } = string.Empty;
            public double LineTotal { get; set; }
            public string WarehouseCode { get; set; } = string.Empty;
        }
        public override string ToString()
        {
            return $"[DocEntry: {DocEntry}, DocNum: {DocNum}, CardCode: {CardCode}," +
                $" Total: {DocTotal}, CardName: {CardName}, DocTotal: {DocTotal}";
        }
    }
}
