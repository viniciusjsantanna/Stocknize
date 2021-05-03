using Stocknize.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Repositories
{
    public interface IMovimentationRepository : IGenericRepository<Movimentation>
    {
        Task<IList<Movimentation>> GetMovimentations(CancellationToken cancellationToken);
    }
}
