using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;

        public ProductManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<Product> AddOrUpdateProductAsync(Product product)
        {
            var existingProduct = await _manager.Product.GetByBarcodeAsync(product.ProductBarcode);

            if (existingProduct != null)
            {
                existingProduct.Stock += product.Stock > 0 ? product.Stock : 1;
                await _manager.Product.UpdateProductAsync(existingProduct);
            }
            else
            {
                product.Stock = product.Stock > 0 ? product.Stock : 1;
                await _manager.Product.CreateProductAsync(product);
            }

            await _manager.SaveAsync();
            return existingProduct ?? product;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var product = await _manager.Product.GetProductByIdAsync(id, trackChanges: true);
            if (product == null)
                return null;

            await _manager.Product.DeleteProductAsync(product);
            await _manager.SaveAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _manager.Product.GetAllProductsAsync(trackChanges: false);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _manager.Product.GetProductByIdAsync(id, trackChanges: false);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            await _manager.Product.UpdateProductAsync(product);
            await _manager.SaveAsync();
            return product;
        }
    }
}
