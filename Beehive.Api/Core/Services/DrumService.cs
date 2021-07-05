using Beehive.Api.Core.Models.Domain;
using Beehive.Api.Infrastructure.Clients;
using Beehive.Api.Infrastructure.Repositories;

namespace Beehive.Api.Core.Services
{
    public class DrumService : IDrumService
    {
        private readonly IDrumClient _client;
        private readonly IRepository<Drum> _repository;

        public DrumService(IRepository<Drum> repository, IDrumClient client)
        {
            _repository = repository;
            _client = client;
        }

        public GetDrumsResponseDto Get(GetDrumsQueryDto query)
        {
            var result = new GetDrumsResponseDto();

            var responseFromApi = _client.GetDrumsForWarehouse(query.WarehouseNumber);
            foreach (var drum in responseFromApi)
            {
                _repository.Add(drum);

                var drumResponse = new DrumDto
                {
                    Label = drum.Label,
                    Size = drum.Size,
                    WarehouseNumber = drum.WarehouseNumber
                };

                result.Drums.Add(drumResponse);
            }

            return result;
        }
    }
}