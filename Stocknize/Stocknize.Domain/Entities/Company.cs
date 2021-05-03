namespace Stocknize.Domain.Entities
{
    public class Company : BaseEntity
    {
        public Company() { }

        public Company(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
