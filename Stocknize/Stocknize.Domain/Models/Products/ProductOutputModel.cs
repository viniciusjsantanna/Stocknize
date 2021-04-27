using System;

namespace Stocknize.Domain.Models.Products
{
    public record ProductOutputModel(Guid Id, string Name, decimal Price, string Type);
}
