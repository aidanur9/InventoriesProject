using InventoriesProject.Models;
using InventoriesProject.Repositories;
using InventoriesProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierService;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService)
        {
            _logger = logger;
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var list = await _supplierService.GetAllSuppliers();
            if (list is null)
            {
                return StatusCode(500, "There is error while getting all suppliers");
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSupplier(Guid id)
        {
            var supplier = await _supplierService.DetailSupplier(id);
            if (supplier is null) return NotFound($"Supplier is not found using id: {id}");

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierViewModel model)
        {
            var model_ = await _supplierService.CreateSupplier(model);
            if (model_ is null) return StatusCode(500, "There is error while creating supplier");

            return Ok(model_);
        }

        [HttpPut]
        public async Task<IActionResult> EditSupplier(SupplierViewModel model)
        {
            var model_ = await _supplierService.EditSupplier(model);
            if (model_ is null) return StatusCode(500, "There is error while creating supplier");

            return Ok(model_);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            await _supplierService.DeleteSupplier(id);

            return Ok("Success deleting supplier");
        }
    }
}
