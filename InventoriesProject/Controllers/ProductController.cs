using InventoriesProject.Models;
using InventoriesProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var list = await _productService.GetAllProducts();
            if (list is null)
            {
                return StatusCode(500, "There is error while getting all products");
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var supplier = await _productService.DetailProduct(id);
            if (supplier is null) return NotFound($"Product is not found using id: {id}");

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel model)
        {
            var model_ = await _productService.CreateProduct(model);
            if (model_ is null) return StatusCode(500, "There is error while creating product");

            return Ok(model_);
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct(ProductViewModel model)
        {
            var model_ = await _productService.EditProduct(model);
            if (model_ is null) return StatusCode(500, "There is error while creating product");

            return Ok(model_);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProduct(id);

            return Ok("Success deleting products");
        }
    }
}
