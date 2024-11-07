using InventoriesProject.Models;

namespace InventoriesProject.Services
{
    public interface IInventoryService
    {
        Task<InventoryViewModel?> CreateInventory(InventoryViewModel model);

        Task<InventoryViewModel?> EditInventory(InventoryViewModel model);

        Task<InventoryViewModel?> DetailInventory(Guid id);

        Task DeleteInventory(Guid id);

        Task<List<InventoryViewModel>?> GetAllInventories();
    }
}
