using System.ComponentModel.DataAnnotations;

namespace InventoriesProject.Data
{
    public class Product : BaseEntity
    {

        [StringLength(100)]
        public string ProductName { get; set; } = default!;

        public float Price { get; set; }

        public Guid SupplierId { get; set; }
    }
}
