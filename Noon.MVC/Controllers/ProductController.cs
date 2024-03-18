using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using AliExpress.Dtos.Category;
using AliExpress.Dtos.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AliExpress.Dtos.ViewResult;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
//using testNoon.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Noon.MVC.Controllers
{

    public class ProductController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ICategoryService _categoryService;
        private IProductService _productService;
        private readonly IWebHostEnvironment _environment;
        public ProductController(ICategoryService categoryService ,IProductService productService, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = categoryService;
            _productService = productService;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

      

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var product = await _productService.GetAllProducts("",1, 50);
            return View(product.Entities);
            //return View();
        }
        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: ProductController/Create
        public async Task<ActionResult> Create()
        {
            var cat = await(_categoryService.GetAllCategory());
            ViewBag.Cat = cat;
            ViewBag.Shipment = new Dictionary<int, string>
                            {
                                { 0, "Free Shipping" },
                                { 1, "Paid Shipping" },
                                { 2, "Express Shipping" }
                            };
            return View();
        }
        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUpdateDeleteProductDto createUpdateDeleteProductDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.Create(createUpdateDeleteProductDto);
                    if (result.IsSuccess)
                    {
                        //TempData["product"] = result.Entity;
                        TempData["id"] = result.Entity.Id;
                        
                        

                        return RedirectToAction("Upload",result.Entity.Id);
                    }
                    else
                    {
                        ViewBag.Error = result.Message;
                        return View(createUpdateDeleteProductDto);
                    }
                }
                else
                {
                    return View(createUpdateDeleteProductDto);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(createUpdateDeleteProductDto);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////// //
        public async Task<IActionResult> Upload()
        {
          

          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "photos");

            // Ensure the directory exists, if not, create it
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            List<string> uploadedFilePaths = new List<string>();

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                {
                    ModelState.AddModelError("files", "One or more files are empty.");
                    return View(); // Return to the view with error message
                }

                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("files", "Only image files (jpg, jpeg, png, gif) are allowed.");
                    return View(); // Return to the view with error message
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Add the full path of the uploaded file to the list
                uploadedFilePaths.Add(filePath.Replace(_environment.WebRootPath, "").Replace("\\", "\\\\"));
            }





            int id = (int)TempData["id"];
            var r = await _productService.GetOne(id);
            CreateUpdateDeleteProductDto product = r.Entity;

            
            product.Images = uploadedFilePaths;

           var result = await _productService.Update(product);

            // Redirect to the index action after successful update
            return RedirectToAction("Index");
        }


        public async Task<CreateUpdateDeleteProductDto> f(int id)
        {
            var resultView = await _productService.GetOne(id);
            CreateUpdateDeleteProductDto product = resultView.Entity;
            return product;
        }



















        //// //////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: ProductController/Edit/5
        public async Task< ActionResult> Edit(int id)
        {
            var product=await _productService.GetOne(id);
            var cat = await (_categoryService.GetAllCategory());
            ViewBag.Cat = cat;
            ViewBag.Shipment = new Dictionary<int, string>
                            {
                                { 0, "Free Shipping" },
                                { 1, "Paid Shipping" },
                                { 2, "Express Shipping" }
                            };
            return View(product.Entity);
        }
        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(int id, CreateUpdateDeleteProductDto productDetailsDto)
        {
            try
            {
                var result = await _productService.Update(productDetailsDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = result.Message;
                }
                return View(productDetailsDto);
            }
            catch
            {
                ViewBag.Error = "An unexpected error occurred.";
                return View("Error");
            }
        }
        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CreateUpdateDeleteProductDto productDto)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> SfotDelete(int id)
        {
            try
            {
               var re= await _productService.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }



    }
}
