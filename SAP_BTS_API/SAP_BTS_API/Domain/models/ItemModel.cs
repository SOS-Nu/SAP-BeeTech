
namespace SAP_BTS_API.domain.models.mappers
{
    public class ItemModel
    {

        public string? ItemName;

        public string ItemCode { get; set; } = string.Empty;
        public string? SalesUnit { get; set; } = string.Empty;
        public double? InStock { get; set; }
        public string? BarCode { get; set; } = string.Empty;
        public string? WarehouseCode { get; set; } = string.Empty;
        public int? CustomsGroupCode  {get; set; }
        public List<ItemPrices>? ItemPrices { get; set; }

    }
    public class ItemPrices
    {
        public int? PriceList { get; set; }
        public double? Price { get; set; }
        public string? Currency { get; set; }
    }
}
