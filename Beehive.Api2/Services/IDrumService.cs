using Beehive.Api2.Models.DTOs;

namespace Beehive.Api2.Services;

public interface IDrumService
{
    public Task<DrumDto> CreateAsync(DrumDto drumToCreate);
}