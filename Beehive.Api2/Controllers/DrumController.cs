using Beehive.Api2.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Beehive.Api2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DrumController : ControllerBase
{
    // GET: api/Drum
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
    public void Post([FromBody] DrumDto value)
    {
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