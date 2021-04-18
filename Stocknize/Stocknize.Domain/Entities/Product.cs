using Stocknize.Domain.Enums;

namespace Stocknize.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(string name, decimal price, ProductType type)
        {
            Name = name;
            Price = price;
            Type = type;
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ProductType Type { get; set; }
    }
}
