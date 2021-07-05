using System.Collections.Generic;
using Beehive.Api.Core.Models.Domain;

namespace Beehive.Api.Infrastructure.Clients
{
    public interface IDrumClient
    {
        public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber);
    }
}