// using VideoGameTrading.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VideoGameTrading.Models;

namespace VideoGameTrading.Controllers
{
    public class HomeController : Controller
    {
        // readonly IShopRepository _repository;

        // public HomeController(IShopRepository r)
        // {
        //     _repository = r;
        // }

        // public IActionResult IndicatorShop()
        // {
        //     var items = _repository.GetItems();

        //     return View(items);
        // }

        public IActionResult Index() => View();

        public IActionResult SiteMap() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
