using Stocknize.Domain.Entities;
using Stocknize.Domain.Models.Jwt;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Domain
{
    public interface IJwtGenerator
    {
        Task<JwtModel> GenerateToken(User user);
    }
}
