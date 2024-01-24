using Application.Interfaces.Services;
using Domain.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<ProductResponse>> Get()
        {
            return Ok(_productService.GetProducts().Data.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] ProductValidator payload)
        {
            var result = await _productService.CreateProduct(payload);
            return result.IsSucess ? Ok(result.Data) : ValidationProblem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "BadRequest",
                detail: result.Error._description);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetById(int id)
        {
            var result = await _productService.GetProductById(id);
            return result.IsSucess ? Ok(result.Data) : Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "BadRequest",
                detail: result.Error._description); ;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> Update(int id, [FromBody] ProductValidator payload)
        {
            var result = await _productService.UpdateProduct(id, payload);
            return result.IsSucess ? Ok(result.Data) : Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "BadRequest",
                detail: result.Error._description); ;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductResponse>> Delete(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return result.IsSucess ? NoContent() : Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "BadRequest",
                detail: result.Error._description);
        }
    }
}
