using AliExpress.Application.IServices;
using AliExpress.Dtos.Category;
using AliExpress.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Noon.MVC.Controllers
{
    public class CategoryController : Controller
    {

        public ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        // GET: CategoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(CategoryDto categoryDto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.Create(categoryDto);

                    if (result!=null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = result;
                    }
                }

                // If ModelState is not valid, return the view with the provided data
                return View(categoryDto);
            }
            catch
            {
                // Return an error view or redirect to a generic error page
                ViewBag.Error = "An unexpected error occurred.";
                return View("Error");
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
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

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
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
