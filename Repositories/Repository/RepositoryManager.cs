using Repositories.Contracts;
using Repositories.EfCore;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private IProductRepository _productRepository;

        public RepositoryManager(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepository Product => _productRepository ??= new ProductRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
