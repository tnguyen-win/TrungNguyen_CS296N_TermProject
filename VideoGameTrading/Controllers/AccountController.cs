﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VideoGameTrading.Models;

namespace VideoGameTrading.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userMngr, SignInManager<AppUser> signInMgr)
        {
            _userManager = userMngr;
            _signInManager = signInMgr;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username, Name = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) return RedirectToAction("Index", "Home");
                else foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }

            ModelState.AddModelError("", "Invalid NAME / USERNAME / PASSWORD / CONFIRM PASSWORD");

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnURL = "")
        {
            var model = new LoginVM { ReturnUrl = returnURL };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded) return (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) ? Redirect(model.ReturnUrl) : RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid USERNAME / PASSWORD");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public ViewResult AccessDenied() => View();
    }
}
