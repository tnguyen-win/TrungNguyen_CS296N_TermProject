using VideoGameTrading.Data;
using VideoGameTrading.Models;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameTrading.Controllers
{
    public class CartController : Controller
    {
        readonly IShopRepository repository;

        public CartController(IShopRepository r) => repository = r;

        [HttpGet]
        public IActionResult Index()
        {
            List<Item> items = (from m in repository.GetItems()
                                where m.InCart == true
                                select m).ToList();

            return View(items);
        }
    }
}
