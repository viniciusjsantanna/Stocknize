using System;

namespace Stocknize.Domain.Models.Company
{
    public record CompanyOutputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
