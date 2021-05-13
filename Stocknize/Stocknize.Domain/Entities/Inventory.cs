namespace Stocknize.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public Inventory()
        {
        }

        public Inventory(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public void ChangeQuantity(int quantity)
        {
            if(quantity < 0)
            {
                throw new System.Exception("Você não tem estoque suficiente para realizar essa movimentação!");
            }
            Quantity = quantity;
        }
    }
}
