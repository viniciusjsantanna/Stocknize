using Stocknize.Domain.Entities;
using System;

namespace Stocknize.Domain.Models.Products
{
    public record ProductInputModel(string Name, decimal Price, Guid ProductTypeId)
    {
        public Guid CompanyId { get; set; }
    }
}
