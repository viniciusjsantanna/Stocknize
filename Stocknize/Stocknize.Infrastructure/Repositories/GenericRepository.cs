using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly EFContext context;
        protected readonly DbSet<T> entities;

        public GenericRepository(EFContext context)
        {
            this.context = context;
            entities = this.context.Set<T>();
        }

        public async Task<T> Add(T entity, CancellationToken cancellationToken)
        {
            await entities.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public Task<bool> Any(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            return entities.AnyAsync(expression, cancellationToken);
        }

        public async Task<T> Delete(T entity, CancellationToken cancellationToken)
        {
            entities.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public Task<T> Get(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            return entities.FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<T> Update(T entity, CancellationToken cancellationToken)
        {
            entities.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
