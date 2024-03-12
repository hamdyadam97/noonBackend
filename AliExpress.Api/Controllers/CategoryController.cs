using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using AliExpress.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AliExpressContext _context;
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService, AliExpressContext aliExpress )
        {
            _categoryService = categoryService;
            _context = aliExpress;
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
            var result = await _categoryService.Delete(id);
            if (!result.IsSuccess)
            {
                return NotFound(); // Return 404 if the category is not found or deletion fails
            }
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}


