using System;

namespace Stocknize.Domain.Models.Products
{
    public class ProductOutputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
    }
}
