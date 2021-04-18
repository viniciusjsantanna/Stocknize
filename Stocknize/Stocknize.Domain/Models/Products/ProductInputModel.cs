using Stocknize.Domain.Enums;

namespace Stocknize.Domain.Models.Products
{
    public class ProductInputModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }
    }
}
