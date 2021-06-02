using Stocknize.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IList<Product>> GetProducts(Guid companyId, CancellationToken cancellationToken);
    }
}
