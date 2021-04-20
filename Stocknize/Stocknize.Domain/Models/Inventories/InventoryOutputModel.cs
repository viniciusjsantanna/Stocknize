using System;

namespace Stocknize.Domain.Models.Inventories
{
    public class InventoryOutputModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ProductType { get; set; }
    }
}
