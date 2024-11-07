using System.ComponentModel.DataAnnotations;

namespace InventoriesProject.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; } = default!;

        public float Price { get; set; }

        public Guid SupplierId { get; set; }
    }
}
