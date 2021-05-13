using Stocknize.Domain.Enums;
using System;

namespace Stocknize.Domain.Models.Inventories
{
    public record MovimentationOutputModel(Guid id, int Quantity)
    {
        public string Product { get; set; }
        public string User { get; set; }
        public string Type { get; set; }
    }
}
