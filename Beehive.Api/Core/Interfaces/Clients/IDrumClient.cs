using System.Collections.Generic;
using Beehive.Api.Core.Models;

namespace Beehive.Api.Core.Interfaces.Clients
{
    public interface IDrumClient
    {
        public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber);
    }
}