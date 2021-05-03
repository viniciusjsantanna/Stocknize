namespace Stocknize.Domain.Entities
{
    public class User : BaseEntity
    {
        public User() { }

        public User(string name, string cpf, Credentials credentials)
        {
            Name = name;
            Cpf = cpf;
            Credentials = credentials;
        }

        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public Credentials Credentials { get; set; }
    }
}
