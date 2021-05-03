using System;

namespace Stocknize.Domain.Models.Products
{
    public record ProductOutputModel(Guid Id, string Name, decimal Price)
    {
        public string Type { get; set; }
        public string CompanyName { get; set; }
    };
}
