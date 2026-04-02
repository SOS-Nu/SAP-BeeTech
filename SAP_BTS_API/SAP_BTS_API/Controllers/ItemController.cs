using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SAP_BTS_API.Applications.Services;
using SAP_BTS_API.Domain.Interfaces.IServices;


namespace SAP_BTS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;


        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = Request.QueryString.Value?.TrimStart('?');
            var result = await _itemService.GetItemsAsync(query);
            return Ok(result);
        }
    }
}
