using Beehive.Api2.DataAccess;
using Beehive.Api2.Exceptions;
using Beehive.Api2.Models.DTOs;
using Beehive.Api2.Models.Entities;

namespace Beehive.Api2.Services;

public class DrumService : IDrumService
{
    private readonly DrumDbContext _context;

    public DrumService(DrumDbContext context)
    {
        _context = context;
    }

    public async Task<DrumDto> CreateAsync(DrumDto drumToCreate)
    {
        var any = _context.Drums.Any(x => x.Id == drumToCreate.Id);

        if (any)
        {
            throw new EntityExistsException();
        }

        var entity = new Drum
        {
            Label = drumToCreate.Label,
            WarehouseNumber = drumToCreate.WarehouseNumber
        };

        var result = _context.Drums.Add(entity);
        await _context.SaveChangesAsync();

        var dto = new DrumDto();
        dto.Id = result.Entity.Id;
        dto.Label = result.Entity.Label;
        dto.WarehouseNumber = result.Entity.WarehouseNumber;

        return dto;
    }
}