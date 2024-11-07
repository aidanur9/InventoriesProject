using InventoriesProject.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoriesProject.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SupplierRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Supplier> CreateSupplier(Supplier supplier)
        {
            _applicationDbContext.Suppliers
                .Add(supplier);

            await _applicationDbContext.SaveChangesAsync();

            return supplier;
        }

        public async Task<Supplier> EditSupplier(Supplier editedSupplier)
        {
            _applicationDbContext.Update(editedSupplier);

            await _applicationDbContext.SaveChangesAsync();

            return editedSupplier;
        }

        public async Task<List<Supplier>> GetAllSuppliers()
        {
            var suppliers = await _applicationDbContext.Suppliers.ToListAsync();
            return suppliers;
        }

        public async Task<Supplier?> GetSingleSupplier(Guid id)
        {
            var supplier = await _applicationDbContext.Suppliers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return supplier;
        }

        public async Task DeleteSupplier(Guid id)
        {
            var supplier = await GetSingleSupplier(id);
            if (supplier is null) throw new InvalidOperationException($"There is no matching supplier with id: {id}");

            _applicationDbContext.Suppliers.Remove(supplier);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
