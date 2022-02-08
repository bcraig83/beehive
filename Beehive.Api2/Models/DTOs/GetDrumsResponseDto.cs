namespace Beehive.Api2.Models.DTOs;

public class GetDrumsResponseDto
{
    public IList<DrumDto> Drums { get; set; } = new List<DrumDto>();
}