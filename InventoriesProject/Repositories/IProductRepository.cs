using InventoriesProject.Data;

namespace InventoriesProject.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateProduct(Product product);

        Task<Product> EditProduct(Product editedProduct);

        Task<Product?> GetSingleProduct(Guid id);

        Task<List<Product>> GetAllProducts();

        Task DeleteProduct(Guid id);
    }
}
