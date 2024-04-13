using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using AliExpress.Dtos.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
//using testNoon.Models;

namespace Noon.MVC.Controllers
{

    public class ProductController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ICategoryService _categoryService;
        private IProductService _productService;
        private readonly IWebHostEnvironment _environment;
        public ProductController(ICategoryService categoryService, IProductService productService, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = categoryService;
            _productService = productService;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }



        // GET: ProductController
        [Authorize(Roles = "admin, vendor")]
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 1);

            // Store the current page number in the session
            HttpContext.Session.SetInt32("CurrentPageNumber", pageNumber);

            var product = await _productService.GetAllProducts("","", pageNumber, 10,-1,-1,"");
            return View(product.Entities);
            //return View();
        }
        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: ProductController/Create
        public async Task<ActionResult> Create(List<IFormFile> files)

        {
            var cat = await (_categoryService.GetAllCategory());
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
        public async Task<ActionResult> Create(CreateUpdateDeleteProductDto createUpdateDeleteProductDto, List<IFormFile> files)
        {
            var cat = await (_categoryService.GetAllCategory());
            ViewBag.Cat = cat;
            ViewBag.Shipment = new Dictionary<int, string>
                            {
                                { 0, "Free Shipping" },
                                { 1, "Paid Shipping" },
                                { 2, "Express Shipping" }
                            };
            try
            {
                //if (ModelState.IsValid)
                //{
                //var result = await _productService.Create(createUpdateDeleteProductDto);
                var result = await _productService.Create(createUpdateDeleteProductDto);
                if (result.IsSuccess)
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

                    #region stroe images lsit in database
                    //int id = (int)TempData["id"];
                    var product = result.Entity;
                    product.Images = uploadedFilePaths;
                    await _productService.Update(product);

                    #endregion
                    TempData["id"] = product.Id; //send to upload image action 
                    return View();
                }
                else
                {
                    ViewBag.Error = result.Message;
                    return View(createUpdateDeleteProductDto);
                }
                //}
                //else
                //{
                //    return View(createUpdateDeleteProductDto);
                //}
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

            #region s
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
        public async Task<ActionResult> Edit(int id)
        {
            var product = await _productService.GetOne(id);
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
        public async Task<ActionResult> Edit(int id, CreateUpdateDeleteProductDto productDetailsDto)
        {
            try
            {
                var product = await _productService.GetOne(id);
                var cat = await (_categoryService.GetAllCategory());
                ViewBag.Cat = cat;
                ViewBag.Shipment = new Dictionary<int, string>
                            {
                                { 0, "Free Shipping" },
                                { 1, "Paid Shipping" },
                                { 2, "Express Shipping" }
                            };
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
                var re = await _productService.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> Next()
        {

            var numProducts = await _productService.countProducts();


            int currentPageNumber = HttpContext.Session.GetInt32("CurrentPageNumber") ?? 1;
            int nextPageNumber = currentPageNumber;

            if ((numProducts / 10) + 1 > currentPageNumber)
                nextPageNumber = currentPageNumber + 1;

            // Redirect to the Index action with the next page number
            return RedirectToAction("Index", new { page = nextPageNumber });
        }
        public async Task<ActionResult> DetailsProduct(int id)
        {
            var product = await _productService.GetOne(id);
            var cat = await (_categoryService.GetAllCategory());
            ViewBag.Cat = cat;
            return View(product.Entity);
        }
        // POST: ProductController/DetailsProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DetailsProduct(int id, CreateUpdateDeleteProductDto updatedProductDetails)
        {
            return View(updatedProductDetails);
        }
        public async Task<ActionResult> Previous()
        {
            int currentPageNumber = HttpContext.Session.GetInt32("CurrentPageNumber") ?? 1;
            int nextPageNumber = currentPageNumber;

            if (currentPageNumber > 1)
                nextPageNumber = currentPageNumber - 1;

            // Redirect to the Index action with the next page number
            return RedirectToAction("Index", new { page = nextPageNumber });
        }

    }
}
