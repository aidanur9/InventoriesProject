using InventoriesProject.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoriesProject.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public InventoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Inventory> CreateInventory(Inventory inventory)
        {
            _applicationDbContext.Inventories
                .Add(inventory);

            await _applicationDbContext.SaveChangesAsync();

            return inventory;
        }

        public async Task<Inventory> EditInventory(Inventory editedInventory)
        {
            _applicationDbContext.Update(editedInventory);

            await _applicationDbContext.SaveChangesAsync();

            return editedInventory;
        }

        public async Task<List<Inventory>> GetAllInventory()
        {
            var inventories = await _applicationDbContext.Inventories.ToListAsync();
            return inventories;
        }

        public async Task<Inventory?> GetSingleInventory(Guid id)
        {
            var inventory = await _applicationDbContext.Inventories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return inventory;
        }

        public async Task DeleteInventory(Guid id)
        {
            var inventory = await GetSingleInventory(id);
            if (inventory is null) throw new InvalidOperationException($"There is no matching supplier with id: {id}");

            _applicationDbContext.Inventories.Remove(inventory);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
