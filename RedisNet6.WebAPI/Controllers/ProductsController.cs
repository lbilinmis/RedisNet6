using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisNet6.Cache;
using RedisNet6.WebAPI.Models;
using RedisNet6.WebAPI.Repositories;
using StackExchange.Redis;

namespace RedisNet6.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        //private readonly IDatabase _database;

        //private readonly RedisService _redisService;

        //public ProductsController(IProductRepository productRepository, RedisService redisService)
        //{
        //    _productRepository = productRepository;
        //    _redisService = redisService;
        //    var db = _redisService.GetDatabase(0);
        //    db.StringSet("name", "levent");

        //}
        //public ProductsController(IProductRepository productRepository, IDatabase database)
        //{
        //    _productRepository = productRepository;
        //    _database = database;

        //}


        public ProductsController(IProductRepository productRepository )
        {
            _productRepository = productRepository;

        }


        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            return Created(string.Empty, await _productRepository.CreateAsync(product));
        }
    }
}

