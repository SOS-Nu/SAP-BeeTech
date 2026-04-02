using Newtonsoft.Json;
using SAP_Integration.Core.Configurations;
using SAP_Integration.Core.domain.models.Mappers;
using SAP_Integration.Core.infrastructure.SAP.Extentions;
using SAP_Integration.Core.Interfaces.Services;
using SAP_Integration.Core.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
public class BusinessPartnerRepositorySL : IBusinessPartnerRepository
{
    public BaseResponse<bool> Update(BusinessPartnerModel bp)
    {
        try
        {
            // Dùng cách này để lấy lỗi trực tiếp, không bị thông báo chung chung
            return UpdateAsync(bp).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
           
            var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return BaseResponse<bool>.Failure(-500, $"Error system: {msg}");
        }
    }

    public async Task<BaseResponse<bool>> UpdateAsync(BusinessPartnerModel bp)
    {

        var dto = BusinessPartnerMapper.ToSLBussinessPartnerDTO(bp);
        var json = JsonConvert.SerializeObject(dto, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        //new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
        //nếu ko có setting này sap sẽ bơ nó đi ko gửi nữa, nếu ko có nó sẽ gửi kèm là null và db sẽ bị trống

        //var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"BusinessPartners('{Uri.EscapeDataString(bp.CardCode)}')");


        var uri = $"BusinessPartners('{Uri.EscapeDataString(bp.CardCode)}')";

        var response = await ServiceLayerConnector.ExecuteWithRetryAsync(async (client) =>
        {
            var request = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PatchAsync(uri, request);
        });

        // Xử lý Session 401 như cũ
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            ServiceLayerConnector.ResetSession();
            return await UpdateAsync(bp);
        }

        return response.IsSuccessStatusCode
            ? BaseResponse<bool>.Ok(true)
            : BaseResponse<bool>.Failure((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}