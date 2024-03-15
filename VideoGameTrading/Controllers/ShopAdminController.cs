using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameTrading.Data;
using VideoGameTrading.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace VideoGameTrading.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShopAdminController : Controller
    {
        private readonly IShopRepository _repository1;

        private readonly IShopLengthRepository _repository2;

        private readonly ICartLengthRepository _repository3;

        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public ShopAdminController(IShopRepository r1, IShopLengthRepository r2, ICartLengthRepository r3, AppDbContext context, UserManager<AppUser> u)
        {
            _repository1 = r1;
            _repository2 = r2;
            _repository3 = r3;
            _context = context;
            _userManager = u;
        }

        // GET: ShopAdmin
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

            return _context.Items != null ? View(await _context.Items.ToListAsync()) : Problem("Entity set 'AppDbContext.Items' is null.");
        }

        // GET: ShopAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
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

            if (id == null || _context.Items == null) NotFound();

            var item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == id);

            if (item == null) return NotFound();

            return View(item);
        }

        // GET: ShopAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            if (id == null || _context.Items == null) return NotFound();

            var item = await _repository1.GetItemByIdAsync(id.Value);

            if (item == null) return NotFound();

            return View(item);
        }

        // POST: ShopAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string fromId, [Bind("ImageId,ItemId,InCart,Title,Genre,ReleaseYear,Price,AgeRange,Condition")] Item item)
        {
            if (id != item.ItemId) return NotFound();

            var from = await _userManager.FindByIdAsync(fromId);

            item.From = from;

            ModelState.ClearValidationState(nameof(Item.From));

            TryValidateModel(item);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        // GET: ShopAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            if (id == null || _context.Items == null) return NotFound();

            var item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == id);

            if (item == null) return NotFound();

            return View(item);
        }

        // POST: ShopAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null) return Problem("Entity set 'AppDbContext.Items' is null.");

            Item Item = _repository1.GetItemByIdAsync(id).Result;

            if (Item != null) _context.Items.Remove(Item);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id) => (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
    }
}
