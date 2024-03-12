using AliExpress.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {

        private readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
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
    }
}
