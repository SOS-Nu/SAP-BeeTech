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
    public class BusinessPartnerController : ControllerBase
    {
        private readonly IBusinessPartnerService _businessPartnerService;
        private readonly IMapper _mapper;


        public BusinessPartnerController(IBusinessPartnerService businessPartnerService, IMapper mapper)
        {
            _businessPartnerService = businessPartnerService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = Request.QueryString.Value?.TrimStart('?');
            var result = await _businessPartnerService.GetCustomersAsync(query);
            return Ok(result);
        }

    }
}
