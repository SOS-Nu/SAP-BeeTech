using AutoMapper;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.Interfaces.IRepositories;
using SAP_BTS_API.Domain.Interfaces.IServices;
using SAP_BTS_API.Infrastructure.persistence.SAP_DTOs.SAPreponses;
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPrequests;
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPresponses;

namespace SAP_BTS_API.Infrastructure.Persistence.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ISapServiceLayerClient _sapClient;
        private readonly IMapper _mapper;

        public ItemRepository(ISapServiceLayerClient sapClient, IMapper mapper)
        {
            _sapClient = sapClient;
            _mapper = mapper;
        }
        public async Task<List<ItemModel>> GetItemsAsync(string? queryString)
        {
            var endpoint = $"Items?{queryString}";
            var sapResponse = await _sapClient.GetAsync<SapCollectionResponse<ItemResponseSAP>>(endpoint);
            return _mapper.Map<List<ItemModel>>(sapResponse?.Value ?? new());
        }

    }
}