using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EFContext context) : base(context) { }

        public async Task<User> GetUserWithCompany(string login, CancellationToken cancellationToken)
        {
            return await entities.Include(e => e.Company).FirstOrDefaultAsync(e => e.Credentials.Login.Equals(login));
        }
    }
}
