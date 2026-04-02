using AutoMapper;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.Interfaces.IRepositories;
using SAP_BTS_API.Domain.Interfaces.IServices;
using SAP_BTS_API.Infrastructure.persistence.SAP_DTOs.SAPreponses;
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPrequests;
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPresponses;

namespace SAP_BTS_API.Infrastructure.Persistence.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly ISapServiceLayerClient _sapClient;
        private readonly IMapper _mapper;

        public SalesOrderRepository(ISapServiceLayerClient sapClient, IMapper mapper)
        {
            _sapClient = sapClient;
            _mapper = mapper;
        }

        public async Task<SalesOrderModel?> CreateAsync(SalesOrderModel order)
        {
            // BƯỚC 1: Map từ Domain Model sang SAP Request DTO (PascalCase)
            var sapRequest = _mapper.Map<SalesOrderRequestSAP>(order);

            // BƯỚC 2: Gửi cho SAP qua Client "Xịn"
            // - Nó tự đính kèm Cookie (từ AuthHandler)
            // - Nó tự serialize sang JSON PascalCase (nhờ _jsonOptions trong Client)
            // - Nó tự kiểm tra lỗi (EnsureSuccessAsync) và ném Exception cho Middleware
            // - Nó tự deserialize từ JSON SAP trả về thành SalesOrderResponseSAP
            var sapResponse = await _sapClient.PostAsJsonAsync<SalesOrderRequestSAP, SalesOrderResponseSAP>("Orders", sapRequest);

            if (sapResponse == null) return null;

            // BƯỚC 3: Map kết quả từ SAP ngược lại Domain Model để trả về
            return _mapper.Map<SalesOrderModel>(sapResponse);
        }

        public async Task<List<SalesOrderModel>> GetOrdersAsync(string queryString)
        {
            // Chỉ cần base endpoint, còn lại "hưởng" hết từ client gửi lên
            var endpoint = $"Orders?{queryString}";

            var sapResponse = await _sapClient.GetAsync<SapCollectionResponse<SalesOrderResponseSAP>>(endpoint);

            return _mapper.Map<List<SalesOrderModel>>(sapResponse?.Value ?? new());
        }
    }
}