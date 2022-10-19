using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using store_api.Models;
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


        [HttpGet("test")]
        public IActionResult Test()
        {
            // System.Console.WriteLine("-------------------------------");
            var claims = new List<object>();
            foreach (var e in User.Claims)
            {
                claims.Add(new
                {
                    Type = e.Type,
                    ValueType = e.ValueType,
                    Value = e.Value,
                    Issuer = e.Issuer,
                });
            }
            //     System.Console.WriteLine($"{e.Type} : {e.ValueType} - {e.Value}");
            // System.Console.WriteLine("-------------------------------");
            return Ok(claims);
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
        [Authorize("AdminPolicy")]
        public IActionResult UpdateProduct([FromBody] Product p)
        {
            _productService.UpdateProduct(p);
            return Ok(p);
        }

        [HttpPost]
        [Authorize("AdminPolicy")]
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