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
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(EFContext context) : base(context) { }

        public async Task<IList<Inventory>> GetInventories(Guid companyId, CancellationToken cancellationToken)
        {
            return await entities.Include(e => e.Product).ThenInclude(e => e.Category).Where(e => e.Product.Company.Id.Equals(companyId)).ToListAsync();
        }
    }
}
