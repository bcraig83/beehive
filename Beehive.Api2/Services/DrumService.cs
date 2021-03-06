using Beehive.Api2.Clients;
using Beehive.Api2.DataAccess;
using Beehive.Api2.Exceptions;
using Beehive.Api2.Models.DTOs;
using Beehive.Api2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beehive.Api2.Services;

public class DrumService : IDrumService
{
    private readonly IDrumClient _client;
    private readonly DrumDbContext _context;

    public DrumService(
        DrumDbContext context,
        IDrumClient client)
    {
        _context = context;
        _client = client;
    }

    public async Task<IEnumerable<DrumDto>> GetAllAsync()
    {
        var responseFromApi = _client.GetDrumsForWarehouse(4);
        _context.Drums.AddRange(responseFromApi);
        await _context.SaveChangesAsync();

        var entityList = await _context.Drums.ToListAsync();

        return entityList
            .Select(item => new DrumDto
            {
                Id = item.Id,
                Label = item.Label,
                WarehouseNumber = item.WarehouseNumber
            })
            .ToList();
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

        var dto = new DrumDto
        {
            Id = result.Entity.Id,
            Label = result.Entity.Label,
            WarehouseNumber = result.Entity.WarehouseNumber
        };

        return dto;
    }
}