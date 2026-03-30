using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.domain.Models.DTOs
{
    public class SapItemUpdateRequest
    {
        [JsonProperty("ItemName")]
        public string ItemName { get; set; }

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