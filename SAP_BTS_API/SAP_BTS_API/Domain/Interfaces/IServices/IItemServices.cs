using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;

namespace SAP_BTS_API.Domain.Interfaces.IServices
{
    public interface IItemService
    {
        Task<BaseResponse<List<ItemResponseDTO>>> GetItemsAsync(string? queryString);

    }
}
