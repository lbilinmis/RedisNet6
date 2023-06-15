using RedisNet6.WebAPI.Models;
using RedisNet6.WebAPI.Repositories;

namespace RedisNet6.WebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllProductsAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await (_repository.GetByIdAsync(id));    
        }
    }
}
