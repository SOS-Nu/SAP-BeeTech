using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;

namespace SAP_BTS_API.Domain.Interfaces.IServices
{
    public interface ISalesOrderService
    {
        Task<BaseResponse<SalesOrderResponseDTO>> CreateOrder(SalesOrderModel order);
        Task<BaseResponse<List<SalesOrderResponseDTO>>> GetOrdersAsync(string? queryString);
     }
}
