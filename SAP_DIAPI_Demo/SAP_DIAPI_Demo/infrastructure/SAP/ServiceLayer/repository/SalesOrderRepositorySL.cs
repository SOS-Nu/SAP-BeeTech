using Newtonsoft.Json;
using SAP_DIAPI_Demo.domain.models.Mappers;
using SAP_DIAPI_Demo.domain.models.Mappers;
using SAP_DIAPI_Demo.infrastructure.SAP.Extentions;
using SAP_DIAPI_Demo.Interfaces.Repository;
using SAP_DIAPI_Demo.Models;
using SAP_DIAPI_Demo.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Repository.RepositorySL
{
    public class SalesOrderRepositorySL : ISalesOrderRepository
    {
        public BaseResponse<SalesOrderResponse> Create(SalesOrderModel order)
        {
            try
            {
                return CreateAsync(order).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BaseResponse<SalesOrderResponse>.Failure(-500, $"Error system: {msg}");
            }
        }

        public async Task<BaseResponse<SalesOrderResponse>> CreateAsync(SalesOrderModel order)
        {
            var dto = order.ToSLSalesOrderDTO();
            var uri = "Orders";

            var response = await ServiceLayerConnector.ExecuteWithRetryAsync(async (client) =>
            {
                var json = JsonConvert.SerializeObject(dto, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                var request = new StringContent(json, Encoding.UTF8, "application/json");

                return await client.PostAsync(uri, request);
            });

            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<SalesOrderResponse>(responseBody);
                return BaseResponse<SalesOrderResponse>.Ok(data);
            }
            else
            {
                return BaseResponse<SalesOrderResponse>.Failure((int)response.StatusCode, responseBody);
            }
        }
    }
}