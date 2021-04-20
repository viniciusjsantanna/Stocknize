using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EFContext context) : base(context)
        {

        }

        public async Task<IList<Product>> GetProducts(CancellationToken cancellationToken)
        {
            return await entities.ToListAsync(cancellationToken);
        }
    }
}
