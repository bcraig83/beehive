using System.Collections.Generic;
using Beehive.Api.Core.Models.Domain;

namespace Beehive.Api.Core.Services
{
    public interface IDrumService
    {
        public GetDrumsResponseDto Get(GetDrumsQueryDto query);
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