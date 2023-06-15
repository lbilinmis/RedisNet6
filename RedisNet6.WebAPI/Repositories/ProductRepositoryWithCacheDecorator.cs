using RedisNet6.Cache;
using RedisNet6.WebAPI.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisNet6.WebAPI.Repositories
{

    public class ProductRepositoryWithCacheDecorator : IProductRepository
    {
        private const string keyProduct = "CacheRedisProduct";
        private readonly IProductRepository _repository;
        private readonly RedisService _redisService;
        private readonly IDatabase _cacheRepository;

        public ProductRepositoryWithCacheDecorator(IProductRepository repository, RedisService redisService)
        {
            _repository = repository;
            _redisService = redisService;
            _cacheRepository = _redisService.GetDatabase(1);
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            var product = await _repository.CreateAsync(entity);

            if (await _cacheRepository.KeyExistsAsync(keyProduct))
            {
                await _cacheRepository.HashSetAsync(keyProduct, entity.Id, JsonSerializer.Serialize(product));
            }

            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            if (!await _cacheRepository.KeyExistsAsync(keyProduct))

                return await LoadToCacheFromDbAync();
            var cacheProducts = await _cacheRepository.HashGetAllAsync(keyProduct);
            var products = new List<Product>();
            foreach (var item in cacheProducts.ToList())
            {
                var product = JsonSerializer.Deserialize<Product>(item.Value);
                products.Add(product);
            }

            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            if (!await _cacheRepository.KeyExistsAsync(keyProduct))
            {
                var product = await _cacheRepository.HashGetAsync(keyProduct, id);
                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;

            }

            var products = await LoadToCacheFromDbAync();

            return products.FirstOrDefault(x => x.Id == id);

        }

        private async Task<List<Product>> LoadToCacheFromDbAync()
        {
            var products = await _repository.GetAllProductsAsync();

            products.ForEach(p =>
            {
                _cacheRepository.HashSetAsync(keyProduct, p.Id, JsonSerializer.Serialize(p));
            });

            return products;
        }
    }
}
