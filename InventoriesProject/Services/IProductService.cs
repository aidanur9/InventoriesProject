using InventoriesProject.Models;

namespace InventoriesProject.Services
{
    public interface IProductService
    {
        Task<ProductViewModel?> CreateProduct(ProductViewModel model);

        Task<ProductViewModel?> EditProduct(ProductViewModel model);

        Task<ProductViewModel?> DetailProduct(Guid id);

        Task DeleteProduct(Guid id);

        Task<List<ProductViewModel>?> GetAllProducts();
    }
}
