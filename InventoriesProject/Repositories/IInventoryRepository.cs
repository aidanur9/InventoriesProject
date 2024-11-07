using InventoriesProject.Data;

namespace InventoriesProject.Repositories
{
    public interface IInventoryRepository
    {
        Task<Inventory> CreateInventory(Inventory inventory);

        Task<Inventory> EditInventory(Inventory editedProduct);

        Task<Inventory?> GetSingleInventory(Guid id);

        Task<List<Inventory>> GetAllInventory();

        Task DeleteInventory(Guid id);
    }
}
