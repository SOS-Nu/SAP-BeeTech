using SAP_BTS_API.Applications.DTOs.requests;
using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;

namespace SAP_BTS_API.Domain.Interfaces.IServices
{
    public interface IBusinessPartnerService
    {
        Task<BaseResponse<List<BusinessPartnerResponseDTO>>> GetCustomersAsync(string? queryString);

    }
}
