using InventoriesProject.Data;

namespace InventoriesProject.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier> CreateSupplier(Supplier supplier);

        Task<Supplier> EditSupplier(Supplier editedSupplier);

        Task<Supplier?> GetSingleSupplier(Guid id);

        Task<List<Supplier>> GetAllSuppliers();

        Task DeleteSupplier(Guid id);
    }
}
