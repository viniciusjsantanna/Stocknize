using System;

namespace Stocknize.Domain.Models.Company
{
    public record CompanyOutputModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
