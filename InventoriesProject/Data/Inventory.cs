namespace InventoriesProject.Data
{
    public class Inventory : BaseEntity
    {
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
