using AutoMapper;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.Interfaces.IRepositories;
using SAP_BTS_API.Domain.Interfaces.IServices;
using SAP_BTS_API.Infrastructure.persistence.SAP_DTOs.SAPreponses;
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPrequests;
using SAP_BTS_API.Infrastructure.Persistence.SAP_DTOs.SAPresponses;

namespace SAP_BTS_API.Infrastructure.Persistence.Repositories
{
    public class BusinessPartnerRepository : IBusinessPartnerRepository
    {
        private readonly ISapServiceLayerClient _sapClient;
        private readonly IMapper _mapper;

        public BusinessPartnerRepository(ISapServiceLayerClient sapClient, IMapper mapper)
        {
            _sapClient = sapClient;
            _mapper = mapper;
        }

        public async Task<List<BusinessPartnerModel>> GetCustomersAsync(string? queryString)
        {
            // SAP SL dùng endpoint "BusinessPartners" cho cả Khách hàng và Nhà cung cấp
            // Mẹo: Thêm filter mặc định CardType = 'C' nếu chỉ muốn lấy khách hàng
            var endpoint = string.IsNullOrWhiteSpace(queryString)
                           ? "BusinessPartners?$filter=CardType eq 'cCustomer'"
                           : $"BusinessPartners?{queryString}";

            var sapResponse = await _sapClient.GetAsync<SapCollectionResponse<BusinessPartnerResponseSAP>>(endpoint);
            return _mapper.Map<List<BusinessPartnerModel>>(sapResponse?.Value ?? new());
        }
    }
}