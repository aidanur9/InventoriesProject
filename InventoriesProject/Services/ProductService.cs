using InventoriesProject.Data;
using InventoriesProject.Models;
using InventoriesProject.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace InventoriesProject.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger; 
        private readonly IProductRepository _productRepository;
        
        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<ProductViewModel?> CreateProduct(ProductViewModel model)
        {
            try
            {
                var product = await _productRepository.CreateProduct(new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = model.ProductName,
                    Price = model.Price,
                    SupplierId = model.SupplierId,
                    DateCreated = DateTime.Now
                });

                model.Id = product.Id;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating product. Error details: {0}", ex.Message);

                return null;
            }
        }

        public async Task DeleteProduct(Guid id)
        {
            try
            {
                await _productRepository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting product. Error details: {0}", ex.Message);
            }
        }

        public async Task<ProductViewModel?> DetailProduct(Guid id)
        {
            try
            {
                var product = await _productRepository.GetSingleProduct(id);
                if (product is null) return null;

                var supplierModel = new ProductViewModel
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    SupplierId = product.SupplierId
                };

                return supplierModel;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while getting single product. Error details: {0}", ex.Message);
                return null;
            }
        }

        public async Task<ProductViewModel?> EditProduct(ProductViewModel model)
        {
            try
            {
                var existingProduct = await _productRepository.GetSingleProduct(model.Id);
                if (existingProduct is null) throw new InvalidOperationException($"There is no matching product with Id: {model.Id}");

                var editedProduct = new Product
                {
                    Id = model.Id,
                    ProductName = string.IsNullOrEmpty(model.ProductName) ? existingProduct.ProductName : model.ProductName,
                    Price = model.Price,
                    SupplierId = existingProduct.SupplierId,
                    DateCreated = existingProduct.DateCreated,
                    DateModified = DateTime.Now,
                };

                await _productRepository.EditProduct(editedProduct);

                model.ProductName = editedProduct.ProductName;
                model.Price = editedProduct.Price;
                model.SupplierId = existingProduct.SupplierId;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while trying to edit product. Error details: {0}", ex.Message);
                return null;
            }
        }

        public async Task<List<ProductViewModel>?> GetAllProducts()
        {
            try
            {
                var list = await _productRepository.GetAllProducts();

                var modelList = list
                    .Select(i => new ProductViewModel
                    {
                        Id = i.Id,
                        ProductName = i.ProductName,
                        Price = i.Price,
                        SupplierId = i.SupplierId,
                    })
                    .ToList();

                return modelList;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while trying to get all products. Error details: {0}", ex.Message);
                return null;
            }
        }
    }
}
