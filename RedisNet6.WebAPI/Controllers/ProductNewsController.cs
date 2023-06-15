using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisNet6.WebAPI.Models;
using RedisNet6.WebAPI.Services;

namespace RedisNet6.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductNewsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductNewsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            return Created(string.Empty, await _productService.CreateAsync(product));
        }

    }
}
