using System.Text.Json.Serialization;

namespace SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPresponses
{
    public class ItemResponseSAP
    {
        [JsonPropertyName("ItemCode")]
        public string? ItemCode { get; set; } // Mã hàng (Key)

        [JsonPropertyName("ItemName")]
        public string? ItemName { get; set; } // Tên hàng

        [JsonPropertyName("ForeignName")]
        public string? ForeignName { get; set; } // Tên ngoại ngữ

        [JsonPropertyName("ItemsGroupCode")]
        public int? ItemsGroupCode { get; set; } // Nhóm hàng hóa

        [JsonPropertyName("SalesUnitMsr")]
        public string? SalesUnit { get; set; } // Đơn vị tính bán hàng (UoM)

        [JsonPropertyName("InventoryItem")]
        public string? InventoryItem { get; set; } // tYES/tNO (Có quản lý kho không)

        [JsonPropertyName("QuantityOnStock")]
        public double? InStock { get; set; } // Tổng tồn kho (Toàn bộ kho)

        [JsonPropertyName("DefaultWarehouse")]
        public string? DefaultWarehouse { get; set; } // Kho mặc định

        [JsonPropertyName("BarCode")]
        public string? BarCode { get; set; }

        [JsonPropertyName("VatGroupPu")]
        public string? PurchaseVatGroup { get; set; } // Thuế đầu vào mặc định

        [JsonPropertyName("VatGroupSa")]
        public string? SalesVatGroup { get; set; } // Thuế đầu ra mặc định

        // Một số trường giá cơ bản (Nếu SAP trả về danh sách giá)
        [JsonPropertyName("ItemPrices")]
        public List<ItemPriceSAP>? ItemPrices { get; set; }
    }

    public class ItemPriceSAP
    {
        [JsonPropertyName("PriceList")]
        public int PriceList { get; set; } // Số thứ tự bảng giá

        [JsonPropertyName("Price")]
        public double? Price { get; set; } // Giá

        [JsonPropertyName("Currency")]
        public string? Currency { get; set; }
    }
}