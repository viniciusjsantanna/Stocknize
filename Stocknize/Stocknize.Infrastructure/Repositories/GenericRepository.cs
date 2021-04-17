using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly EFContext context;
        private readonly DbSet<T> dbset;

        public GenericRepository(EFContext context)
        {
            this.context = context;
            dbset = this.context.Set<T>();
        }

        public async Task<T> Add(T entity, CancellationToken cancellationToken)
        {
            await dbset.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public Task<T> Delete(T Entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return dbset.Where(expression);
        }

        public async Task<IList<T>> GetAll(CancellationToken cancellationToken, Expression<Func<T, object>> includes = null)
        {
            var query = dbset.AsNoTracking();

            if (includes is not null)
            {
                query.Include(includes);
            }

            return await query.ToListAsync(cancellationToken);

        }

        public Task<T> Update(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
