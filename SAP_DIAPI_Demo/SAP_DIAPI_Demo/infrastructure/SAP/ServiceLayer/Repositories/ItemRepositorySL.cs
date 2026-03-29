//using Newtonsoft.Json;
//using SAP_DIAPI_Demo.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace SAP_DIAPI_Demo.Repository.RepositorySL
//{
//    public class ItemRepositorySL : IItemSerViceLayerRepository
//    {
//        private readonly HttpClient _httpClient;

//        public ItemRepositorySL(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<BaseResponse<bool>> UpdatePriceSL(ItemModel priceData)
//        {
//            // Service Layer dùng OData. Để update giá, ta gọi vào Item -> ItemPrices
//            var updatePayload = new
//            {
//                ItemPrices = new[]
//                {
//                new { PriceList = priceData.PriceList, Price = priceData.Price, Currency = priceData.Currency }
//            }
//            };

//            var json = JsonConvert.SerializeObject(updatePayload);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");

//            var response = await _httpClient.PutAsync($"Items('{priceData.ItemCode}')", content);

//            if (response.IsSuccessStatusCode)
//                return BaseResponse<bool>.Ok(true);

//            return BaseResponse<bool>.Failure((int)response.StatusCode, "Lỗi từ SAP Service Layer");
//        }
//    }
//}
