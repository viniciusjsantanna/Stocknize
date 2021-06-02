using Stocknize.Domain.Enums;

namespace Stocknize.Domain.Entities
{
    public class Movimentation : BaseEntity
    {
        public Movimentation() { }

        public Movimentation(MovimentationType type, int quantity, Product product, User user)
        {
            Type = type;
            Quantity = quantity;
            Product = product;
            User = user;
        }

        public MovimentationType Type { get; private set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
