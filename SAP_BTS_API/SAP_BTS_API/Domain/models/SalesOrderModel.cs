using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SAP_BTS_API.domain.models.mappers
{
    public class SalesOrderModel
    {
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string CardName { get; set; } = string.Empty;
        public double DocTotal { get; set; }

        public string CardCode { get; set; }
        public DateTime DocDueDate { get; set; }
        public string Comments { get; set; }
        public int BPL_IDAssignedToInvoice { get; set; }
        public List<SalesOrderItemModel> Lines { get; set; } = new List<SalesOrderItemModel>();
    }

    public class SalesOrderItemModel
    {
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string WarehouseCode { get; set; }
        public string Currency { get; set; } = string.Empty;

    }

}