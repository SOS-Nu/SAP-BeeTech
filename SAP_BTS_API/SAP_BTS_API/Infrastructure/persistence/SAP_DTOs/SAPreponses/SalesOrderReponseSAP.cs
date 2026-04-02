using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPresponses
{

    public class SalesOrderResponseSAP
    {
        [JsonPropertyName("odata.metadata")]
        public string? OdataMetadata { get; set; }

        [JsonPropertyName("DocEntry")]
        public int DocEntry { get; set; }

        [JsonPropertyName("DocNum")]
        public int DocNum { get; set; }

        [JsonPropertyName("DocStatus")]
        public string? DocStatus { get; set; } // bost_Open, bost_Close...

        [JsonPropertyName("DocDate")]
        public string? DocDate { get; set; }

        [JsonPropertyName("DocDueDate")]
        public string? DocDueDate { get; set; }

        [JsonPropertyName("CardCode")]
        public string? CardCode { get; set; }

        [JsonPropertyName("CardName")]
        public string? CardName { get; set; }

        [JsonPropertyName("DocTotal")]
        public double DocTotal { get; set; }

        [JsonPropertyName("VatSum")]
        public double VatSum { get; set; }

        [JsonPropertyName("DocCurrency")]
        public string? DocCurrency { get; set; }

        [JsonPropertyName("Comments")]
        public string? Comments { get; set; }

        [JsonPropertyName("BPL_IDAssignedToInvoice")]
        public int? BPLId { get; set; }

        [JsonPropertyName("DocumentLines")]
        public List<SalesOrderLineResponseSAP> DocumentLines { get; set; } = new();


    }

    public class SalesOrderLineResponseSAP
    {
        [JsonPropertyName("LineNum")]
        public int LineNum { get; set; }

        [JsonPropertyName("ItemCode")]
        public string? ItemCode { get; set; }

        [JsonPropertyName("ItemDescription")]
        public string? ItemDescription { get; set; }

        [JsonPropertyName("Quantity")]
        public double Quantity { get; set; }

        [JsonPropertyName("UnitPrice")]
        public double UnitPrice { get; set; }

        [JsonPropertyName("PriceAfterVAT")]
        public double PriceAfterVat { get; set; }

        [JsonPropertyName("WarehouseCode")]
        public string? WarehouseCode { get; set; }

        [JsonPropertyName("LineTotal")]
        public double LineTotal { get; set; }

        [JsonPropertyName("VatGroup")]
        public string? VatGroup { get; set; }
    }
}