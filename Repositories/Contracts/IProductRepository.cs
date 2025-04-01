using Entities.Models;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
        Task<Product> GetProductByIdAsync(int productId, bool trackChanges);
        Task<Product> GetByBarcodeAsync(string productBarcode);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
