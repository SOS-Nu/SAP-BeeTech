using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SAP_Integration.Core.domain.Models.DTOs
{
    public class SalesOrderRequest
    {
        [JsonProperty("CardCode")]
        public string CardCode { get; set; }

        [JsonProperty("DocDueDate")] 
        public string DocDueDate { get; set; }

        private string _comments;
        [JsonProperty("Comments", NullValueHandling = NullValueHandling.Ignore)]
        public string Comments
        {
            get => _comments;
            set => _comments = string.IsNullOrWhiteSpace(value) ? null : value;
        }



        [JsonProperty("BPL_IDAssignedToInvoice")]
        public int BPL_IDAssignedToInvoice { get; set; }



        [JsonProperty("DocumentLines")]
        public List<SalesOrderLineRequest> DocumentLines { get; set; } = new List<SalesOrderLineRequest>();
    }

    public class SalesOrderLineRequest
    {
        [JsonProperty("ItemCode")] // Mã hàng (Bắt buộc)
        public string ItemCode { get; set; }

        [JsonProperty("Quantity")] // Số lượng
        public double Quantity { get; set; }

        [JsonProperty("UnitPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }

        [JsonProperty("WarehouseCode", NullValueHandling = NullValueHandling.Ignore)]
        public string WarehouseCode { get; set; }
    }
}