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
                        
                        TempData["id"] = result.Entity.Id;//send to upload iamge action 
                        return RedirectToAction("Upload");
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
            //int id = (int)TempData["id"];
            //var r = await _productService.GetOne(id);
            //CreateUpdateDeleteProductDto product = r.Entity;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {

            #region Upload Image
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
                    return View();
                }

                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("files", "Only image files (jpg, jpeg, png, gif) are allowed.");
                    return View(); 
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                uploadedFilePaths.Add(filePath.Replace(_environment.WebRootPath, "").Replace("\\", "\\\\"));
            }
            #endregion


            #region stroe images lsit in database
            int id = (int)TempData["id"];
            var r = await _productService.GetOne(id);
            CreateUpdateDeleteProductDto product = r.Entity;
            product.Images = uploadedFilePaths;
            var result = await _productService.Update(product);
            #endregion
            
            return RedirectToAction("Index");
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
