using VideoGameTrading.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VideoGameTrading.Models;

namespace VideoGameTrading.Controllers
{
    public class AccountController : Controller
    {
        private readonly IShopRepository _repository1;

        private readonly IShopLengthRepository _repository2;

        private readonly ICartLengthRepository _repository3;

        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IShopRepository r1, IShopLengthRepository r2, ICartLengthRepository r3, AppDbContext context, UserManager<AppUser> userMngr, SignInManager<AppUser> signInMgr)
        {
            _repository1 = r1;
            _repository2 = r2;
            _repository3 = r3;
            _context = context;
            _userManager = userMngr;
            _signInManager = signInMgr;
        }

        public async Task<IActionResult> Register()
        {
            ShopLength shoplength = await _repository2.GetShopLengthByIdAsync(1);
            CartLength cartlength = await _repository3.GetCartLengthByIdAsync(1);

            shoplength.ShopTotal = _repository1.GetItems().Count;
            cartlength.CartTotal = _repository1.GetItems()
            .Where(m => m.InCart == true)
            .ToList().Count;

            _context.SaveChanges();

            ViewBag.ShopLength = shoplength.ShopTotal;
            ViewBag.CartLength = cartlength.CartTotal;

            return View();
        }

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

        public async Task<IActionResult> Login(string returnURL = "")
        {
            ShopLength shoplength = await _repository2.GetShopLengthByIdAsync(1);
            CartLength cartlength = await _repository3.GetCartLengthByIdAsync(1);

            shoplength.ShopTotal = _repository1.GetItems().Count;
            cartlength.CartTotal = _repository1.GetItems()
            .Where(m => m.InCart == true)
            .ToList().Count;

            _context.SaveChanges();

            ViewBag.ShopLength = shoplength.ShopTotal;
            ViewBag.CartLength = cartlength.CartTotal;

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

        public async Task<IActionResult> AccessDenied()
        {
            ShopLength shoplength = await _repository2.GetShopLengthByIdAsync(1);
            CartLength cartlength = await _repository3.GetCartLengthByIdAsync(1);

            shoplength.ShopTotal = _repository1.GetItems().Count;
            cartlength.CartTotal = _repository1.GetItems()
            .Where(m => m.InCart == true)
            .ToList().Count;

            _context.SaveChanges();

            ViewBag.ShopLength = shoplength.ShopTotal;
            ViewBag.CartLength = cartlength.CartTotal;

            return View();
        }
    }
}
