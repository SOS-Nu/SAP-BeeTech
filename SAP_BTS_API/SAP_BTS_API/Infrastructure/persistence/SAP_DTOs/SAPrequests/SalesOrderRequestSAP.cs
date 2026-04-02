using SAP_BTS_API.Applications.DTOs.requests;
using System;
using System.Collections.Generic;

using System.Text.Json.Serialization;

namespace SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPrequests
{
    public class SalesOrderRequestSAP
    {
        public string? CardCode { get; set; }
        public string? DocDueDate { get; set; }

        private string? _comments;
        public string? Comments
        {
            get => _comments;
            set => _comments = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public int BPL_IDAssignedToInvoice { get; set; }
        public List<DocumentLines> DocumentLines { get; set; } = new();
    }

    public class DocumentLines
    {
        public string? ItemCode { get; set; }
        public double Quantity { get; set; }

        // SAP yêu cầu field tên là UnitPrice, nhưng C# bạn đặt là Price nên cần dòng này
        [JsonPropertyName("UnitPrice")]
        public double? Price { get; set; }

        public string? WarehouseCode { get; set; }
    }
}