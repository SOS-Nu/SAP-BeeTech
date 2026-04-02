using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Integration.Core.domain.Models.DTOs
{
    public class ItemUpdateRequest
    {
        private string _itemName;
        [JsonProperty("ItemName", NullValueHandling = NullValueHandling.Include)]
        public string ItemName
        {
            get => _itemName;
            set => _itemName = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        [JsonProperty("ItemPrices")]
        public List<ItemPriceDTO> ItemPrices { get; set; }
    }

    public class ItemPriceDTO
    {
        [JsonProperty("PriceList")]
        public int PriceList { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }
    }
}