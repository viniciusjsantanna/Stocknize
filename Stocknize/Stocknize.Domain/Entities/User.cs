namespace Stocknize.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public Credentials Credentials { get; set; }
    }
}
