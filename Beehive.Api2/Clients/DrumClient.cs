using Beehive.Api2.Models.Entities;

namespace Beehive.Api2.Clients;

public class DrumClient : IDrumClient
{
    public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber)
    {
        // Dummy implementation
        var result = new List<Drum>
        {
            new()
            {
                Label = "ANDSDG123",
                WarehouseNumber = warehouseNumber
            },
            new()
            {
                Label = "fkewre578",
                WarehouseNumber = warehouseNumber
            }
        };

        return result;
    }
}