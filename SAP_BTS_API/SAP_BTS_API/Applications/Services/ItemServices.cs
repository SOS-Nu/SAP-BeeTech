using AutoMapper;
using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;
using SAP_BTS_API.Domain.Interfaces.IRepositories;
using SAP_BTS_API.Domain.Interfaces.IServices;
using System.Net;


namespace SAP_BTS_API.Applications.Services
{


    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<BaseResponse<List<ItemResponseDTO>>> GetItemsAsync(string? queryString)
        {
            var items = await _repository.GetItemsAsync(queryString);
            var responseData = _mapper.Map<List<ItemResponseDTO>>(items);
            return BaseResponse<List<ItemResponseDTO>>.Ok(responseData);
        }

    }
}