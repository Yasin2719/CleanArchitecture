//using Domain.DTOs.Categories;
//using Application.Interfaces.Services;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace API.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class CatgeoriesController : ControllerBase
//    {
//        private readonly ICategoryService _categoryService;

//        public CatgeoriesController(ICategoryService categoryService)
//        {
//            _categoryService = categoryService;
//        }

//        [HttpGet]
//        public ActionResult<List<CategoryResponse>> Get()
//        {
//            return Ok(_categoryService.GetCategories().ToList());
//        }

//        [HttpPost]
//        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CategoryValidator payload)
//        {
//            var category = await _categoryService.CreateCategory(payload);
//            return category != null ? Ok(category) : NotFound();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<CategoryResponse>> GetById(int id)
//        {
//            var category = await _categoryService.GetCategoryById(id);
//            return category != null ? Ok(category) : NotFound();
//        }

//        [HttpPut("{id}")]
//        public async Task<ActionResult<CategoryResponse>> Update(int id, [FromBody] CategoryValidator payload)
//        {
//            try
//            {
//                var category = await _categoryService.UpdateCategory(id, payload);
//                return category != null ? Ok(category) : NotFound();
//            }
//            catch (Exception e)
//            {
//                return BadRequest(e.Message);
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delete(int id)
//        {
//            await _categoryService.DeleteCategory(id);
//            return StatusCode(204);
//        }
//    }
//}