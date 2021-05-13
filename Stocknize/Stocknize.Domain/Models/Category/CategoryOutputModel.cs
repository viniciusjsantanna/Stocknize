using System;

namespace Stocknize.Domain.Models.Category
{
    public record CategoryOutputModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
