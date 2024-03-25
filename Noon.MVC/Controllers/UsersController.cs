﻿using AliExpress.Dtos.Product;
using AliExpress.Dtos.User;
using AliExpress.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Noon.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UsersController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: UsersController
        public ActionResult Index()
        {
       
            var allUsers = _userManager.Users.ToList(); 
            var userDTOs = _mapper.Map<List<AppUser>, List<UserDTO>>(allUsers);
            return View(userDTOs);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
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

        public async Task<ActionResult> Deactivate(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Deactivate = true;
            await _userManager.UpdateAsync(user);
            
            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Activate(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Deactivate = false;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        // GET: UsersController/Delete/5
        public async Task< ActionResult> Delete(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            user.Deactivate = true;
            return RedirectToAction(nameof(Index));
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
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
