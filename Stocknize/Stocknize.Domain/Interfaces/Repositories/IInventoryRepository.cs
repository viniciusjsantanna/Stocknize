using Stocknize.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Repositories
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        Task<IList<Inventory>> GetInventories(CancellationToken cancellationToken);
    }
}
