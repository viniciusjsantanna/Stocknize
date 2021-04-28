using Stocknize.Domain.Enums;

namespace Stocknize.Domain.Entities
{
    public class Movimentation : BaseEntity
    {
        public MovimentationType Type { get; private set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
