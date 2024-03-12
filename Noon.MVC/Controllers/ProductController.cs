using AliExpress.Application.IServices;
using AliExpress.Application.Services;
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
        public ActionResult Index()
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
