using Beehive.Api2.Models.Entities;

namespace Beehive.Api2.Clients;

public interface IDrumClient
{
    public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber);
}