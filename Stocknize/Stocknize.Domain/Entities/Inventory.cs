namespace Stocknize.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
    }
}
