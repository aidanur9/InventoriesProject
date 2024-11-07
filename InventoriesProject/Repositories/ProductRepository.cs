using InventoriesProject.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoriesProject.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _applicationDbContext.Products
                .Add(product);

            await _applicationDbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> EditProduct(Product editedProduct)
        {
            _applicationDbContext.Update(editedProduct);

            await _applicationDbContext.SaveChangesAsync();

            return editedProduct;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _applicationDbContext.Products.ToListAsync();
            return products;
        }

        public async Task<Product?> GetSingleProduct(Guid id)
        {
            var product = await _applicationDbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await GetSingleProduct(id);
            if (product is null) throw new InvalidOperationException($"There is no matching supplier with id: {id}");

            _applicationDbContext.Products.Remove(product);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
