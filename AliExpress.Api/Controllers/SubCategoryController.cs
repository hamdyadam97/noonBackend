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

    public class SubCategoryController : ControllerBase
    {
        private readonly AliExpressContext _context;

        private readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService,AliExpressContext aliExpress)
        {
            _subCategoryService = subCategoryService;
            _context = aliExpress;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCategory()
        {

            var Cats = await _subCategoryService.GetAllCategory();

            return Ok(Cats);
        }


        [HttpGet("DetailsCategory/{id}")]
        public async Task<ActionResult> DetailsCategory(int id)
        {
            var Cat = await _subCategoryService.GetOne(id);
            if (Cat == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            return Ok(Cat);
        }


        [HttpDelete("DeleteSubCategory/{id}")]
        public async Task<ActionResult> DeleteSubCategory(int id)
        {
            var result = await _subCategoryService.Delete(id);
            if (!result.IsSuccess)
            {
                return NotFound(); // Return 404 if the category is not found or deletion fails
            }
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
