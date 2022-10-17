using Microsoft.AspNetCore.Mvc;
using store_api.Model;
using store_api.Services;

namespace store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] int? page)
        {
            if (page is null)
            {
                return Ok(_productService.GetProducts());
            }

            int n = page ?? 1;
            return Ok(_productService.GetProductsWithPaging(n, 10));
        }


        [HttpGet("{id}")]
        public IActionResult FindProduct([FromRoute] int id)
        {
            var p = _productService.FindProduct(id);

            if (p is null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product p) {
            _productService.UpdateProduct(p);
            return Ok("updated");
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product p)
        {
            _productService.InsertProduct(p);
            return Ok("created");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}