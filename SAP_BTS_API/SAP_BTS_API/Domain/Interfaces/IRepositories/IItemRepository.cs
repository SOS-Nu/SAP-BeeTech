using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;


namespace SAP_BTS_API.Domain.Interfaces.IRepositories
{
    public interface IItemRepository
    {
        Task<List<ItemModel>> GetItemsAsync(string? queryString);

    }
}
