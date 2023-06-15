using RedisNet6.WebAPI.Models;

namespace RedisNet6.WebAPI.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product entity);
    }
}
