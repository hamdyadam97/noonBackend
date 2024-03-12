using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCategory()
        {
          
            var Cats = await _categoryService.GetAllCategory();

            return Ok(Cats);
        }


        [HttpGet("DetailsCategory/{id}")]
        public async Task<ActionResult> DetailsCategory(int id)
        {
            var Cat = await _categoryService.GetOne(id);
            if (Cat == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            return Ok(Cat);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var Cat = await _categoryService.GetOne(id);
            if (Cat == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }
            else
            {
                _categoryService.Delete(Cat.Entity);

            }
            return Ok(Cat);
        }
    }
}
