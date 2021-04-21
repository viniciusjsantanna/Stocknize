using Stocknize.Domain.Models.Jwt;

namespace Stocknize.Domain.Models.User
{
    public record UserLoggedOutputModel
    {
        public JwtModel JsonWebToken { get; set; }
        public string Name { get; init; }
        public string Login { get; init; }
    }
}
