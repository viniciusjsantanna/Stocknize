using Stocknize.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Repositories
{
    public interface IProductTypeRepository : IGenericRepository<ProductType>
    {
        Task<IList<ProductType>> GetProductTypes(CancellationToken cancellationToken); 
    }
}
