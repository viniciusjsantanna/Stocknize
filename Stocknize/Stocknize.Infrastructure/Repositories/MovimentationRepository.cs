using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Infrastructure.Repositories
{
    public class MovimentationRepository : GenericRepository<Movimentation>, IMovimentationRepository
    {
        public MovimentationRepository(EFContext context) : base(context)
        {

        }

        public async Task<IList<Movimentation>> GetMovimentations(CancellationToken cancellationToken)
        {
            return await entities
                .Include(e => e.Product)
                .Include(e => e.User)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
