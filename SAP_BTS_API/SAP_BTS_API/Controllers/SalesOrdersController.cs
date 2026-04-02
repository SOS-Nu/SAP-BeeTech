using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SAP_BTS_API.Applications.DTOs.requests;
using SAP_BTS_API.Applications.DTOs.responses;
using SAP_BTS_API.Applications.Services;
using SAP_BTS_API.domain.models.mappers;
using SAP_BTS_API.Domain.common;
using SAP_BTS_API.Domain.Interfaces.IServices;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SAP_BTS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrdersController : ControllerBase
    {
        private readonly ISalesOrderService _orderService;
        private readonly IMapper _mapper;


        public SalesOrdersController(ISalesOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SalesOrderRequestDTO request)
        {
            if (request == null)
                return BadRequest(BaseResponse<string>.Failure(HttpStatusCode.BadRequest, "Data not null"));

            var model = _mapper.Map<SalesOrderModel>(request);

            var result = await _orderService.CreateOrder(model);

            return result.isSuccess ? Ok(result) : StatusCode((int)result.statusCode, result);
        }


        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            var rawQuery = Request.QueryString.Value?.TrimStart('?');

            var results = await _orderService.GetOrdersAsync(rawQuery);

            return Ok(results);
        }
    }
}
