using System;

namespace Stocknize.Domain.Models.Products
{
    public class ProductAddOutputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
