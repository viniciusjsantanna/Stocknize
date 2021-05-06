using Stocknize.Domain.Enums;
using System;

namespace Stocknize.Domain.Models.Inventories
{
    public record MovimentationInputModel(Guid ProductId, MovimentationType Type, int Quantity, decimal Price)
    {
        public Guid UserId { get; set; }
    };
}
