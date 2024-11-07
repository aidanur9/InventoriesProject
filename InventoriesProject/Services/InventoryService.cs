using InventoriesProject.Data;
using InventoriesProject.Models;
using InventoriesProject.Repositories;

namespace InventoriesProject.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ILogger<InventoryService> _logger; 
        private readonly IInventoryRepository _inventoryRepository;
        
        public InventoryService(ILogger<InventoryService> logger, IInventoryRepository inventoryRepository)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<InventoryViewModel?> CreateInventory(InventoryViewModel model)
        {
            try
            {
                var inventory = await _inventoryRepository.CreateInventory(new Inventory()
                {
                    Id = Guid.NewGuid(),
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    DateCreated = DateTime.Now
                });

                model.Id = inventory.Id;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating inventory. Error details: {0}", ex.Message);

                return null;
            }
        }

        public async Task DeleteInventory(Guid id)
        {
            try
            {
                await _inventoryRepository.DeleteInventory(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting inventory. Error details: {0}", ex.Message);
            }
        }

        public async Task<InventoryViewModel?> DetailInventory(Guid id)
        {
            try
            {
                var inventory = await _inventoryRepository.GetSingleInventory(id);
                if (inventory is null) return null;

                var inventoryModel = new InventoryViewModel
                {
                    Id = inventory.Id,
                    Quantity = inventory.Quantity,
                    ProductId = inventory.ProductId
                };

                return inventoryModel;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while getting single inventory. Error details: {0}", ex.Message);
                return null;
            }
        }

        public async Task<InventoryViewModel?> EditInventory(InventoryViewModel model)
        {
            try
            {
                var existingInventory = await _inventoryRepository.GetSingleInventory(model.Id);
                if (existingInventory is null) throw new InvalidOperationException($"There is no matching inventory with Id: {model.Id}");

                var editedProduct = new Inventory
                {
                    Id = model.Id,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity < 0 ? existingInventory.Quantity : model.Quantity,
                    DateCreated = existingInventory.DateCreated,
                    DateModified = DateTime.Now
                };

                await _inventoryRepository.EditInventory(editedProduct);

                model.Quantity = editedProduct.Quantity;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while trying to edit inventory. Error details: {0}", ex.Message);
                return null;
            }
        }

        public async Task<List<InventoryViewModel>?> GetAllInventories()
        {
            try
            {
                var list = await _inventoryRepository.GetAllInventory();

                var modelList = list
                    .Select(i => new InventoryViewModel
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                    })
                    .ToList();

                return modelList;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while trying to get all inventories. Error details: {0}", ex.Message);
                return null;
            }
        }
    }
}
