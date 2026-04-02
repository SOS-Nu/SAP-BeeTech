using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;

namespace SAP_BTS_API.Domain.Interfaces.IRepositories
{
    public interface IBusinessPartnerRepository
    {
        Task<List<BusinessPartnerModel>> GetCustomersAsync(string? queryString);

    }
}
