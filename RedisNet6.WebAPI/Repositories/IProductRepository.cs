using RedisNet6.WebAPI.Models;

namespace RedisNet6.WebAPI.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product entity);
    }
}
