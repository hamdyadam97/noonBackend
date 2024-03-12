using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using AliExpress.Context;
using AliExpress.Dtos.Product;
using AliExpress.Dtos.ViewResult;
using AliExpress.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly AliExpressContext _context;
        public ProductController(IProductService productService, AliExpressContext aliExpress)
        {
            _productService = productService;
            _context = aliExpress;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts(int page = 1, string searchTerm = null)
        {
            const int pageSize = 10;
            var Prds = await _productService.GetAllProducts(searchTerm,page,pageSize);

            return Ok(Prds);
        }

       
        [HttpGet("DetailsProduct/{id}")]
        public async Task<ActionResult> DetailsProduct(int id)
        {
            var product = await _productService.GetOne(id);
            if (product == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            return Ok(product);
        }

       
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> AddImages(int productId, List<IFormFile> files)
        {
            var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            foreach (var file in files)
            {
                var image = new Images { Url = file.FileName };
                product.Images.Add(image);
            }

            await _context.SaveChangesAsync();

            return Ok("Images uploaded successfully");
        }
       


      
        [HttpPost]
        public async Task<ActionResult<CreateUpdateDeleteProductDto>> Create(CreateUpdateDeleteProductDto product)
        {
            if (ModelState.IsValid)
            {

                var prd = await _productService.Create(product);
                
                return Created("http://localhost:5164/api/Product/" + product.Id, "Saved");
            }
            return BadRequest(ModelState);
        }



        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteSubCategory(int id)
        {
            var result = await _productService.Delete(id);
            if (!result.IsSuccess)
            {
                return NotFound(); // Return 404 if the category is not found or deletion fails
            }
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
