using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using store_api.Models;
using store_api.Services;
using store_api.Filters;

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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        public IActionResult UpdateProduct([FromBody] Product p)
        {
            _productService.UpdateProduct(p);
            return Ok(p);
        }

        [HttpPost]
        [RoleAuthorize("admin")]
        public IActionResult CreateProduct([FromBody] Product p)
        {
            _productService.InsertProduct(p);
            return Ok(p);
        }

        [HttpDelete("{id}")]
        [Authorize("AdminPolicy")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("bestselling/{limit}")]
        public IActionResult GetBestSelling([FromRoute] int limit)
        {
            var products = _productService.FindTopSoldProduct(limit);
            return Ok(products);
        }

    }
}