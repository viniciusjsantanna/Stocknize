using Stocknize.Domain.Entities;
using Stocknize.Domain.Models.User;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Stocknize.Domain.Interfaces.Domain
{
    public interface IUserService
    {
        Task<User> AddUser(UserInputModel userModel, CancellationToken cancellationToken);

        Task DeleteUser(Guid userId, CancellationToken cancellationToken);

        Task<UserLoggedOutputModel> UpdateUser(Guid userId, UserInputModel userModel, CancellationToken cancellationToken);

        Task<UserLoggedOutputModel> Authenticate(CredentialsInputModel model, CancellationToken cancellationToken);
    }
}
