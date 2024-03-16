using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using AliExpress.Dtos.Category;
using AliExpress.Dtos.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Noon.MVC.Controllers
{
    public class ProductController : Controller
    {

        private ICategoryService _categoryService;
        private IProductService _productService;
        public ProductController(ICategoryService categoryService ,IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
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
                        return RedirectToAction("Index");
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
               // var cat = await _productService.GetOne(id);
                await _productService.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {

                return View("Error");
            }
        }



    }
}
