using System;

namespace Stocknize.Domain.Models.ProductType
{
    public record ProductTypeOutputModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
