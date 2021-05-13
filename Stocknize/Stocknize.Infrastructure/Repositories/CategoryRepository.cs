using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EFContext context) : base(context) { }

        public async Task<IList<Category>> GetProductTypes(CancellationToken cancellationToken)
        {
            return await entities.ToListAsync(cancellationToken);
        }
    }
}
