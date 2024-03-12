using AliExpress.Application.IServices;
using AliExpress.Dtos.Category;
using AliExpress.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

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
        public async Task<ActionResult> Index()
        {

            var catergory = await (_categoryService.GetAllCategory());
            return View(catergory);
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

                    if (result.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = result.Message;
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
        public async Task<ActionResult> Edit(int id)
        {

            var catergory=await (_categoryService.GetOne(id));
            return View(catergory.Entity);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(int id, CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.Update(categoryDto);

                    if (result.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = result.Message;
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
