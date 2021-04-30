using Stocknize.Domain.Enums;
using System;

namespace Stocknize.Domain.Models.Inventories
{
    public record MovimentationInputModel
    {
        public MovimentationInputModel(Guid userId, Guid productId, MovimentationType type, int quantity)
        {
            UserId = userId;
            ProductId = productId;
            Type = type;
            Quantity = quantity;
        }

        public Guid UserId { get; set; }
        public Guid ProductId { get; init; }
        public MovimentationType Type { get; init; }
        public int Quantity { get; init; }
    }
}
