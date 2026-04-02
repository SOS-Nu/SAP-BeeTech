using AutoMapper;
using SAP_BTS_API.Applications.DTOs.requests;
using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;
using SAP_BTS_API.Domain.Interfaces.IRepositories;
using SAP_BTS_API.Domain.Interfaces.IServices;
using System.Net;


namespace SAP_BTS_API.Applications.Services
{


    public class BusinessPartnerService : IBusinessPartnerService
    {
        private readonly IBusinessPartnerRepository _repository;
        private readonly IMapper _mapper;

        public BusinessPartnerService(IBusinessPartnerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<BaseResponse<List<BusinessPartnerResponseDTO>>> GetCustomersAsync(string? queryString)
        {
            var customers = await _repository.GetCustomersAsync(queryString);
            var responseData = _mapper.Map<List<BusinessPartnerResponseDTO>>(customers);
            return BaseResponse<List<BusinessPartnerResponseDTO>>.Ok(responseData);
        }

    }
}