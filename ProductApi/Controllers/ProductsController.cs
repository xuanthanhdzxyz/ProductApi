using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // POST /api/products
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { errors });
            }

            var createdProduct = _productService.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { errors = new List<string> { "ID phải là số nguyên dương" } });
            }

            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound(new { errors = new List<string> { $"Không tìm thấy sản phẩm với ID = {id}" } });
            }

            return Ok(product);
        }
    }
}