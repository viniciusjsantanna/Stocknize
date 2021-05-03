using Stocknize.Domain.Enums;

namespace Stocknize.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ProductType Type { get; set; }
        public Company Company { get; set; }
    }
}
