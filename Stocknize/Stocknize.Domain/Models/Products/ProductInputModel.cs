using Stocknize.Domain.Enums;

namespace Stocknize.Domain.Models.Products
{
    public record ProductInputModel
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public ProductType Type { get; init; }
    }
}
