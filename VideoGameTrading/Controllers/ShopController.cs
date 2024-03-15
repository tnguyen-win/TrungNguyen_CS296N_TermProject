using VideoGameTrading.Data;
using VideoGameTrading.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameTrading.Controllers
{
    public class ShopController : Controller
    {
        readonly IShopRepository _repository1;

        readonly IShopLengthRepository _repository2;

        readonly ICartLengthRepository _repository3;

        readonly AppDbContext _context;

        readonly UserManager<AppUser> _userManager;

        public ShopController(IShopRepository r1, IShopLengthRepository r2, ICartLengthRepository r3, AppDbContext context, UserManager<AppUser> u)
        {
            _repository1 = r1;
            _repository2 = r2;
            _repository3 = r3;
            _context = context;
            _userManager = u;
        }

        // Homepage

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

            var items = _repository1.GetItems();

            return View(items);
        }

        [HttpPost]
        public IActionResult Index(string search, string genre, int releaseYearMin, int releaseYearMax, int priceMin, int priceMax, string ageRange, string condition, string author)
        {
            List<Item> items = (from i in _repository1.GetItems() select i).ToList();

            var matchSearch = false;
            var matchGenre = false;
            var matchReleaseYear = false;
            var matchPrice = false;
            var matchAgeRange = false;
            var matchCondition = false;
            var matchAuthor = false;

            foreach (var i in _repository1.GetItems())
            {
                if (i.Title == search)
                {
                    items.Clear();
                    matchSearch = true;
                }
                if (i.Genre == genre)
                {
                    items.Clear();
                    matchGenre = true;
                }
                if (i.ReleaseYear >= releaseYearMin & i.ReleaseYear <= releaseYearMax)
                {
                    items.Clear();
                    matchReleaseYear = true;
                }
                if (i.Price >= priceMin & i.Price <= priceMax)
                {
                    items.Clear();
                    matchPrice = true;
                }
                if (i.AgeRange == ageRange)
                {
                    items.Clear();
                    matchAgeRange = true;
                }
                if (i.Condition == condition)
                {
                    items.Clear();
                    matchCondition = true;
                }
                if (i.From.Name == author)
                {
                    items.Clear();
                    matchAuthor = true;
                }
            }

            // Search

            if (matchSearch)
            {
                var ITEMS = (from i in _repository1.GetItems()
                             where i.Title == search
                             select i).ToList();

                foreach (var I in ITEMS) items.Add(I);
            }

            // Genre

            if (matchGenre)
            {
                var ITEMS = (from i in _repository1.GetItems()
                             where i.Genre == genre
                             select i).ToList();

                foreach (var I in ITEMS) items.Add(I);
            }

            // Release Year

            if (matchReleaseYear)
            {
                var ITEMS = (from i in _repository1.GetItems()
                             where i.ReleaseYear >= releaseYearMin & i.ReleaseYear <= releaseYearMax
                             select i).ToList();

                foreach (var I in ITEMS) items.Add(I);
            }

            // Price

            if (matchPrice)
            {
                var ITEMS = (from i in _repository1.GetItems()
                             where i.Price >= priceMin & i.Price <= priceMax
                             select i).ToList();

                foreach (var I in ITEMS) items.Add(I);
            }

            // Age Range

            if (matchAgeRange)
            {
                var ITEMS = (from i in _repository1.GetItems()
                             where i.AgeRange == ageRange
                             select i).ToList();

                foreach (var I in ITEMS) items.Add(I);
            }

            // Condition

            if (matchCondition)
            {
                var ITEMS = (from i in _repository1.GetItems()
                             where i.Condition == condition
                             select i).ToList();

                foreach (var I in ITEMS) items.Add(I);
            }

            // Author

            if (matchAuthor)
            {
                var ITEMS = (from i in _repository1.GetItems()
                             where i.From.Name == author
                             select i).ToList();

                foreach (var I in ITEMS) items.Add(I);
            }

            return View("index", items.OrderBy(i => i.ItemId).Distinct().ToList());
        }

        // Create

        [Authorize]
        public async Task<IActionResult> Create()
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
        [Authorize]
        public async Task<IActionResult> Create(Item model)
        {
            Random rnd = new();

            // Fallbacks
            model.Title ??= "Random title";
            model.Genre ??= "Strategy";
            if (model.ReleaseYear == 0) model.ReleaseYear = 2023;
            if (model.Price == 0) model.Price = Math.Round((double)(1 + (rnd.NextDouble() * (100 - 1))), 2);
            model.AgeRange ??= "Everyone";
            model.Condition ??= "Good";
            if (_userManager != null) model.From = await _userManager.GetUserAsync(User);

            // Originals
            model.ImageId = rnd.Next(1, 10);

            await _repository1.StoreItemAsync(model);

            return RedirectToAction("index", new { model.ItemId });
        }

        // Delete

        [Authorize]
        public async Task<IActionResult> DeleteProduct(int itemId)
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

            _repository1.DeleteItem(itemId);

            return RedirectToAction("Index");
        }
    }
}
