using InventoriesProject.Models;

namespace InventoriesProject.Services
{
    public interface ISupplierService
    {
        Task<SupplierViewModel?> CreateSupplier(SupplierViewModel model);

        Task<SupplierViewModel?> EditSupplier(SupplierViewModel model);

        Task<SupplierViewModel?> DetailSupplier(Guid id);

        Task DeleteSupplier(Guid id);

        Task<List<SupplierViewModel>?> GetAllSuppliers();
    }
}
