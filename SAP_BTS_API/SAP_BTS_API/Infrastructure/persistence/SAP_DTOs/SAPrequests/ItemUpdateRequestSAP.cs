using System.Text.Json.Serialization;

namespace SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPrequests
{
    public class ItemUpdateRequestSAP
    {
        private string? _itemName;

        // Ép buộc gửi lên SAP ngay cả khi giá trị là null (để SAP xóa dữ liệu cũ)
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? ItemName
        {
            get => _itemName;
            set => _itemName = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public List<ItemPriceDTO>? ItemPrices { get; set; }
    }

    public class ItemPriceDTO
    {
        public int PriceList { get; set; }
        public double Price { get; set; }
        public string? Currency { get; set; }
    }
}