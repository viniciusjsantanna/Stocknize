using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> Add(T entity, CancellationToken cancellationToken);
        Task<T> Update(T entity, CancellationToken cancellationToken);
        Task<T> Delete(T entity, CancellationToken cancellationToken);
        Task<IList<T>> GetAll(CancellationToken cancellationToken, Expression<Func<T, object>> includes = default);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
    }
}
