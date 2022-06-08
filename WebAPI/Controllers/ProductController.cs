using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {        private readonly IProductManager _productManager;


        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet("getall")]
        public IActionResult GetAllProduct()
        {
            var products = _productManager.GetAllProducts();

            if (products.Count == 0)
                return Ok(new { status = 200, message = "Hec bir mehsul tapilmadi." });

            return Ok(new { status = 200, message = products });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("productlist")]
        public IActionResult ProductList()
        {
            Console.WriteLine("Serverden gelir.");
            var productList = _productManager.GetAllProductList();

            return Ok(new { status = 200, message = productList });
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id){

            var product = _productManager.GetProductById(id);

            return Ok(new {status = 200, message = product});
        }

        [HttpPost("add")]
        public IActionResult AddProduct(AddProductDTO product)
        {
            try
            {
                _productManager.AddProduct(product);
            }
            catch (Exception e)
            {
                return Ok(new { status = 400, message = e });
            }
            return Ok(new { status = 200, message = "Mehsul elave olundu." });
        }
    }
}
