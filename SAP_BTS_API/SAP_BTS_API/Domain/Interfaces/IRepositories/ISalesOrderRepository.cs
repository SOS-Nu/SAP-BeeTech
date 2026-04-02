using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;

namespace SAP_BTS_API.Domain.Interfaces.IRepositories
{
    public interface ISalesOrderRepository
    {
        Task<SalesOrderModel> CreateAsync(SalesOrderModel order);
        Task<List<SalesOrderModel>> GetOrdersAsync(string queryString);
    }
}
