using System.ComponentModel.DataAnnotations;

namespace InventoriesProject.Data
{
    public class Supplier : BaseEntity
    {
        [StringLength(100)]
        public string SupplierName { get; set; } = default!;

        [StringLength(50)]
        public string ContactInfo { get; set; } = default!;
    }
}
