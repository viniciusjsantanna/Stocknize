using Stocknize.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserWithCompany(string login, CancellationToken cancellationToken);
    }
}
