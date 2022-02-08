using System.ComponentModel.DataAnnotations;

namespace Beehive.Api2.Models.Entities;

public class Drum
{
    [Key]
    public int Id { get; set; }

    public string Label { get; set; }
    public int WarehouseNumber { get; set; } = 1;
}