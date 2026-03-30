using Newtonsoft.Json;
using SAP_DIAPI_Demo.domain.models.Mappers;
using SAP_DIAPI_Demo.domain.models.Mappers;
using SAP_DIAPI_Demo.infrastructure.SAP.Extentions;
using SAP_DIAPI_Demo.Interfaces.Repository;
using SAP_DIAPI_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Repository.RepositorySL
{
    public class ItemRepositorySL : IItemRepository
    {
        public BaseResponse<bool> UpdatePrice(ItemModel priceData)
        {
            try
            {
                // Dùng cách này để lấy lỗi trực tiếp, không bị thông báo chung chung
                return UpdateAsync(priceData).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // Nếu có lỗi bên trong, lấy message của lỗi đó
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BaseResponse<bool>.Failure(-500, $"Error system: {msg}");
            }
        }

        public async Task<BaseResponse<bool>> UpdateAsync(ItemModel priceData)
        {
            var dto = priceData.ToSLPriceDTO();
            var json = JsonConvert.SerializeObject(dto, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var uri = $"Items('{Uri.EscapeDataString(priceData.ItemCode)}')";


            var response = await ServiceLayerConnector.ExecuteWithRetryAsync(async (client) =>
            {
                var request = new StringContent(json, Encoding.UTF8, "application/json");

                return await client.PatchAsync(uri, request);
            });

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ServiceLayerConnector.ResetSession();
                return await UpdateAsync(priceData);
            }
            return response.IsSuccessStatusCode
           ? BaseResponse<bool>.Ok(true)
           : BaseResponse<bool>.Failure((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
