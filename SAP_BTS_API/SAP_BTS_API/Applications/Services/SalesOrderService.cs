using AutoMapper;
using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;
using SAP_BTS_API.Domain.Interfaces.IRepositories;
using SAP_BTS_API.Domain.Interfaces.IServices;
using System.Net;



namespace SAP_BTS_API.Applications.Services
{


    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _repository;
        private readonly IMapper _mapper;
        public SalesOrderService(ISalesOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<BaseResponse<SalesOrderResponseDTO>> CreateOrder(SalesOrderModel order)
        {
            var resultModel = await _repository.CreateAsync(order);

            if (resultModel == null)
            {
                return BaseResponse<SalesOrderResponseDTO>.Failure(HttpStatusCode.InternalServerError);
            }

            var finalResponse = _mapper.Map<SalesOrderResponseDTO>(resultModel);
            return BaseResponse<SalesOrderResponseDTO>.Ok(finalResponse);
        }

        public async Task<BaseResponse<List<SalesOrderResponseDTO>>> GetOrdersAsync(string? queryString)
        {
            var orders = await _repository.GetOrdersAsync(queryString);

            if (orders == null)
            {
                return BaseResponse<List<SalesOrderResponseDTO>>.Failure(HttpStatusCode.InternalServerError, "no emty");
            }

          
            var responseData = _mapper.Map<List<SalesOrderResponseDTO>>(orders);

            return BaseResponse<List<SalesOrderResponseDTO>>.Ok(responseData);
        }
    }
}