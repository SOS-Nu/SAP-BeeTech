using System.Text.Json.Serialization;

namespace SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPresponses
{
    public class BusinessPartnerResponseSAP
    {
        [JsonPropertyName("CardCode")]
        public string? CardCode { get; set; } // Mã khách hàng (Key)

        [JsonPropertyName("CardName")]
        public string? CardName { get; set; } // Tên khách hàng

        [JsonPropertyName("CardType")]
        public string? CardType { get; set; } // cCustomer (C), cSupplier (S), cLid (L)

        [JsonPropertyName("GroupCode")]
        public int? GroupCode { get; set; } // Nhóm khách hàng

        [JsonPropertyName("Address")]
        public string? Address { get; set; } // Địa chỉ hóa đơn

        [JsonPropertyName("ZipCode")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("MailAddress")]
        public string? MailAddress { get; set; } // Địa chỉ giao hàng

        [JsonPropertyName("Phone1")]
        public string? Phone1 { get; set; }

        //nó tự lấy trường email address đồ vào Email quá đã
        [JsonPropertyName("EmailAddress")]
        public string? Email { get; set; }

        [JsonPropertyName("ContactPerson")]
        public string? ContactPerson { get; set; } // Người liên hệ mặc định

        [JsonPropertyName("Currency")]
        public string? Currency { get; set; } // Loại tiền tệ (##, VND, USD...)

        [JsonPropertyName("VatIdUnCmp")]
        public string? TaxId { get; set; } // Mã số thuế

        [JsonPropertyName("CurrentAccountBalance")]
        public double? Balance { get; set; } // Công nợ hiện tại

        [JsonPropertyName("PriceListNum")]
        public int? PriceListNum { get; set; } // Bảng giá đang áp dụng
    }
}