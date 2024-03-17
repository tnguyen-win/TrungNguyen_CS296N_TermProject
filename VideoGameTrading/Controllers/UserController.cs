using VideoGameTrading.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VideoGameTrading.Models;
using Microsoft.AspNetCore.Authorization;

namespace VideoGameTrading.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IShopRepository _repository1;

        private readonly IShopLengthRepository _repository2;

        private readonly ICartLengthRepository _repository3;

        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IShopRepository r1, IShopLengthRepository r2, ICartLengthRepository r3, AppDbContext context, UserManager<AppUser> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            _repository1 = r1;
            _repository2 = r2;
            _repository3 = r3;
            _context = context;
            _userManager = userMngr;
            _roleManager = roleMngr;
        }

        public async Task<IActionResult> Index()
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

            List<AppUser> users = new();

            foreach (AppUser user in _userManager.Users.ToList())
            {
                user.RoleNames = await _userManager.GetRolesAsync(user);
                users.Add(user);
            }

            UserVM model = new()
            {
                Users = users,
                Roles = _roleManager.Roles
            };

            return View(model);
        }

        public async Task<IActionResult> Add()
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

            return View("../account/register");
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterVM model)
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

            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) return RedirectToAction("Index");
                else foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }

            return View("../account/register");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
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

            AppUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    string errorMessage = "";

                    foreach (IdentityError error in result.Errors) errorMessage += $"{error.Description} | ";

                    TempData["message"] = errorMessage;
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
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

            IdentityRole adminRole = await _roleManager.FindByNameAsync("Admin");

            if (adminRole == null) TempData["message"] = "Admin role doesn't exist. Click 'Add Admin Role' button to create it.";
            else
            {
                AppUser user = await _userManager.FindByIdAsync(id);

                await _userManager.AddToRoleAsync(user, adminRole.Name);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
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

            AppUser user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");

            if (result.Succeeded) { }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
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

            var result = await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (result.Succeeded) { }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
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

            IdentityRole role = await _roleManager.FindByIdAsync(id);

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded) { }

            return RedirectToAction("Index");
        }

    }
}
