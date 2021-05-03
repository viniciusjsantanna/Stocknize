using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Infrastructure.Repositories
{
    public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(EFContext context) : base(context) { }

        public async Task<IList<ProductType>> GetProductTypes(CancellationToken cancellationToken)
        {
            return await entities.ToListAsync(cancellationToken);
        }
    }
}
