using Newtonsoft.Json;
using SAP_DIAPI_Demo.Configurations;
using SAP_DIAPI_Demo.Interfaces.Services;
using SAP_DIAPI_Demo.Models;
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
            // Nếu có lỗi bên trong, lấy message của lỗi đó
            var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return BaseResponse<bool>.Failure(-500, $"Error system: {msg}");
        }
    }

    public async Task<BaseResponse<bool>> UpdateAsync(BusinessPartnerModel bp)
    {
        var client = await ServiceLayerConnector.GetClientAsync();

        // Chỉ lấy trường cần update, bỏ qua các trường null
        var json = JsonConvert.SerializeObject(new
        {
            bp.CardName,
            bp.Phone1,
            EmailAddress = bp.Email,
            VatIDNum = bp.FederalTaxID
        }, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        // Tạo Request nhanh với Patch Override
        var request = new HttpRequestMessage(HttpMethod.Post, $"BusinessPartners('{Uri.EscapeDataString(bp.CardCode)}')")
        {
            Content = new StringContent(json, new UTF8Encoding(false), "application/json")
        };
        request.Headers.Add("X-HTTP-Method-Override", "PATCH");

        var response = await client.SendAsync(request);

        // Nếu 401 (Hết hạn), reset session để lần sau tự login lại
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            ServiceLayerConnector.ResetSession();
            return await UpdateAsync(bp); // Thử lại 1 lần
        }

        return response.IsSuccessStatusCode
            ? BaseResponse<bool>.Ok(true)
            : BaseResponse<bool>.Failure((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}