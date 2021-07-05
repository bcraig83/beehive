using System;
using System.Collections.Generic;
using Beehive.Api.Core.Models;
using Beehive.Api.Core.Models.Domain;

namespace Beehive.Api.Infrastructure.Clients
{
    public class DrumClient : IDrumClient
    {
        public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber)
        {
            throw new NotImplementedException();
        }
    }
}