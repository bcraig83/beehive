using System.Threading.Tasks;
using Beehive.Api.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Beehive.Api.Presentation
{
    [ApiController]
    [Route("[controller]")]
    public class DrumsController : ControllerBase
    {
        private readonly IDrumService _service;

        public DrumsController(IDrumService service)
        {
            _service = service;
        }

        [HttpGet("{warehouseNumber:int}")]
        public async Task<ActionResult> Get(int warehouseNumber)
        {
            var query = new GetDrumsQueryDto {WarehouseNumber = warehouseNumber};
            var result = await _service.GetAsync(query);

            return Ok(result);
        }
    }
}