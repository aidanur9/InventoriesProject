using System.ComponentModel.DataAnnotations;

namespace InventoriesProject.Models
{
    public class SupplierViewModel
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string SupplierName { get; set; } = default!;

        [StringLength(50)]
        public string ContactInfo { get; set; } = default!;
    }
}
