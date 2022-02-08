using Beehive.Api2.Models.DTOs;
using Beehive.Api2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Beehive.Api2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DrumController : ControllerBase
{
    private readonly IDrumService _drumService;

    // GET: api/Drum
    public DrumController(IDrumService drumService)
    {
        _drumService = drumService;
    }

    [HttpGet]
    public IEnumerable<DrumDto> Get()
    {
        return new List<DrumDto>();
    }

    // GET: api/Drum/5
    [HttpGet("{id}", Name = "Get")]
    public DrumDto Get(int id)
    {
        return new DrumDto();
    }

    // POST: api/Drum
    [HttpPost]
    public async Task<DrumDto> Post([FromBody] DrumDto value)
    {
        return await _drumService.CreateAsync(value);
    }

    // PUT: api/Drum/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] DrumDto value)
    {
    }

    // DELETE: api/Drum/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}