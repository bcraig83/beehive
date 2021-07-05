using System.Collections.Generic;
using System.Threading.Tasks;
using Beehive.Api.Core.Models;

namespace Beehive.Api.Core.Services
{
    public interface IDrumService
    {
        public Task<GetDrumsResponseDto> GetAsync(GetDrumsQueryDto query);
    }

    public class GetDrumsQueryDto
    {
        public int WarehouseNumber { get; set; }
    }

    public class GetDrumsResponseDto
    {
        public IList<DrumDto> Drums { get; set; } = new List<DrumDto>();
    }

    public class DrumDto
    {
        public Size Size { get; set; }
        public int WarehouseNumber { get; set; }
        public string Label { get; set; }
    }
}