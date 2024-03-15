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
        private readonly AppDbContext _context;

        private readonly IShopRepository _repository;

        private readonly UserManager<AppUser> _userManager;

        public ShopAdminController(AppDbContext context, IShopRepository repository, UserManager<AppUser> u)
        {
            _context = context;
            _repository = repository;
            _userManager = u;
        }

        // GET: ShopAdmin
        public async Task<IActionResult> Index() => _context.Items != null ? View(await _context.Items.ToListAsync()) : Problem("Entity set 'AppDbContext.Items' is null.");

        // GET: ShopAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null) NotFound();

            var item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == id);

            if (item == null) return NotFound();

            return View(item);
        }

        // GET: ShopAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null) return NotFound();

            var item = await _repository.GetItemByIdAsync(id.Value);

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

            Item Item = _repository.GetItemByIdAsync(id).Result;

            if (Item != null) _context.Items.Remove(Item);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id) => (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
    }
}
