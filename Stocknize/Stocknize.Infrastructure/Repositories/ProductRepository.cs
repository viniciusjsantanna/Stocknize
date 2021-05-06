using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EFContext context) : base(context) { }

        public async Task<IList<Product>> GetProducts(Guid companyId, CancellationToken cancellationToken)
        {
            return await entities.Include(e => e.Company).Include(e => e.Category)
                .Where(e => e.Company.Id.Equals(companyId)).ToListAsync(cancellationToken);
        }
    }
}
