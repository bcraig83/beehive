using System;

namespace Beehive.Api.Core.Models.Domain
{
    public class Drum
    {
        public string Id { get; } = Guid.NewGuid().ToString();
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