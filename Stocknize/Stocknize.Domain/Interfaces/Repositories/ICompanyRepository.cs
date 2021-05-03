using Stocknize.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<IList<Company>> GetCompanies(CancellationToken cancellationToken);
    }
}
