using Stocknize.Domain.Enums;
using System;

namespace Stocknize.Domain.Models.Inventories
{
    public record MovimentationOutputModel(Guid id, string User, string Product, int Quantity)
    {
        public string Type { get; set; }
    }
}
