namespace Stocknize.Domain.Models.User
{
    public record UserInputModel
    {
        public string Name { get; init; }
        public string Cpf { get; init; }
        public string Login { get; init; }
        public string Password { get; init; }
    }
}
