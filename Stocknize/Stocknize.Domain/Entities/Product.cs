using Stocknize.Domain.Enums;

namespace Stocknize.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ProductType Type { get; set; }
    }
}
