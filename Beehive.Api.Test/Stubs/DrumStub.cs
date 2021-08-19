using System.Collections.Generic;
using Beehive.Api.Core.Interfaces.Clients;
using Beehive.Api.Core.Models;

namespace Beehive.Api.Test.Stubs
{
    public class DrumStub : BaseStub<int, int, IEnumerable<Drum>>, IDrumClient
    {
        public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber)
        {
            RecordedRequests.Add(warehouseNumber);

            var hasResponse = Expectations.TryGetValue(warehouseNumber, out var response);
            return hasResponse ? response : new List<Drum>();
        }
    }
}