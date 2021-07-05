using System.Threading.Tasks;
using Beehive.Api.Infrastructure.Clients;
using Beehive.Api.Infrastructure.DataAccess;

namespace Beehive.Api.Core.Services
{
    public class DrumService : IDrumService
    {
        private readonly IDrumClient _client;
        private readonly IUnitOfWork _unitOfWork;

        public DrumService(
            IDrumClient client,
            IUnitOfWork unitOfWork)
        {
            _client = client;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetDrumsResponseDto> GetAsync(GetDrumsQueryDto query)
        {
            var result = new GetDrumsResponseDto();

            var responseFromApi = _client.GetDrumsForWarehouse(query.WarehouseNumber);
            foreach (var drum in responseFromApi)
            {
                _unitOfWork.DrumRepository.Add(drum);
                await _unitOfWork.SaveChangesAsync();

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