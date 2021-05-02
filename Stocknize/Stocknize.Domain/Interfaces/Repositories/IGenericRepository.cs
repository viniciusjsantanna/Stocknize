using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> Add(T entity, CancellationToken cancellationToken);
        Task<T> Update(T entity, CancellationToken cancellationToken);
        Task Delete(T entity, CancellationToken cancellationToken);
        Task<T> Get(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
        Task<bool> Any(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    }
}
