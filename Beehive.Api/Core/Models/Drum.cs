using System.ComponentModel.DataAnnotations;

namespace Beehive.Api.Core.Models
{
    public class Drum
    {
        [Key]
        public int Id { get; set; }

        public Size Size { get; set; } = Size.Medium;
        public string Label { get; set; }
        public int WarehouseNumber { get; set; } = 1;
    }

    public enum Size
    {
        Large,
        Medium,
        Small
    }
}