using Domain.DTOs.Products;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
            return Ok(_productService.GetProducts().ToList());
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] ProductValidator payload)
        {
            var product = await _productService.CreateProduct(payload);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetById(int id)
        {
            var product = await _productService.GetProductById(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> Update(int id, [FromBody] ProductValidator payload)
        {
            try
            {
                var product = await _productService.UpdateProduct(id, payload);
                return product != null ? Ok(product) : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductResponse>> Delete(int id)
        {
            await _productService.DeleteProduct(id);
            return StatusCode(204);
        }
    }
}
