using Microsoft.AspNetCore.Mvc;
using Domain;
using Services;

namespace WebApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        // PUT api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        // GET api/products/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        // GET api/products/total-price/category/{categoryId}
        [HttpGet("total-price/category/{categoryId}")]
        public async Task<IActionResult> GetTotalPriceByCategory(int categoryId)
        {
            var totalPrice = await _productService.GetTotalPriceByCategoryAsync(categoryId);
            return Ok(totalPrice);
        }
    }
}
