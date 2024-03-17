using VideoGameTrading.Data;
using VideoGameTrading.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace VideoGameTrading.Controllers
{
    public class CartController : Controller
    {
        readonly IShopRepository _repository1;

        readonly IShopLengthRepository _repository2;

        readonly ICartLengthRepository _repository3;

        readonly AppDbContext _context;

        public CartController(IShopRepository r1, IShopLengthRepository r2, ICartLengthRepository r3, AppDbContext context)
        {
            _repository1 = r1;
            _repository2 = r2;
            _repository3 = r3;
            _context = context;
        }

        [Authorize]
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

            List<Item> items = (from i in _repository1.GetItems()
                                where i.InCart == true
                                select i).ToList();

            return View(items);
        }
    }
}
