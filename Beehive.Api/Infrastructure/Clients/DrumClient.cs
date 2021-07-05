using System.Collections.Generic;
using Beehive.Api.Core.Interfaces.Clients;
using Beehive.Api.Core.Models;

namespace Beehive.Api.Infrastructure.Clients
{
    public class DrumClient : IDrumClient
    {
        public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber)
        {
            // Dummy implementation
            var result = new List<Drum>
            {
                new()
                {
                    Label = "ANDSDG123", Size = Size.Small, WarehouseNumber = 1
                }
            };

            return result;
        }
    }
}