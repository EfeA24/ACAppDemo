using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EfCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges)
        {
            return await FindAllAsync(trackChanges);
        }

        public async Task<Product> GetProductByIdAsync(int productId, bool trackChanges)
        {
            var result = await FindByConditionAsync(p => p.ProductId == productId, trackChanges);
            return result.SingleOrDefault();
        }

        public async Task<Product> GetByBarcodeAsync(string productBarcode)
        {
            var result = await FindByConditionAsync(p => p.ProductBarcode == productBarcode, trackChanges: false);
            return result.FirstOrDefault();
        }

        public async Task CreateProductAsync(Product product)
        {
            await CreateAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await DeleteAsync(product);
        }
    }
}
