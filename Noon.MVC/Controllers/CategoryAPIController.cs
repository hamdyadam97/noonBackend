using AliExpress.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Noon.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        public ICategoryService _categoryService;
        public CategoryAPIController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<string> getcat(string name)
        {


            return new List<string> {"ddddd" };
        }
    }
}
