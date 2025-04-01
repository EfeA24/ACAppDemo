using Microsoft.Extensions.DependencyInjection;
using Repositories.Contracts;
using Repositories.Repository;
using Services.Contracts;
using Services;
using Services.Contrats;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IProductService, ProductManager>();
        }
    }
}
