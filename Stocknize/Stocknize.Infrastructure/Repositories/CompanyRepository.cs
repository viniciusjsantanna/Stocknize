using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Infrastructure.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(EFContext context) : base(context) { }

        public async Task<IList<Company>> GetCompanies(CancellationToken cancellationToken)
        {
            return await entities.ToListAsync(cancellationToken);
        }
    }
}
