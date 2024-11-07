using InventoriesProject.Models;
using InventoriesProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories()
        {
            var list = await _inventoryService.GetAllInventories();
            if (list is null)
            {
                return StatusCode(500, "There is error while getting all inventories");
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetInventory(Guid id)
        {
            var inventory = await _inventoryService.DetailInventory(id);
            if (inventory is null) return NotFound($"Inventory is not found using id: {id}");

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory(InventoryViewModel model)
        {
            var model_ = await _inventoryService.CreateInventory(model);
            if (model_ is null) return StatusCode(500, "There is error while creating inventory");

            return Ok(model_);
        }

        [HttpPut]
        public async Task<IActionResult> EditInventory(InventoryViewModel model)
        {
            var model_ = await _inventoryService.EditInventory(model);
            if (model_ is null) return StatusCode(500, "There is error while creating inventory");

            return Ok(model_);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteInventory(Guid id)
        {
            await _inventoryService.DeleteInventory(id);

            return Ok("Success deleting supplier");
        }
    }
}
