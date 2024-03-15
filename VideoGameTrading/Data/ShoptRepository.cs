using VideoGameTrading.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameTrading.Data
{
    public class ShopRepository : IShopRepository
    {
        readonly AppDbContext _context;

        public ShopRepository(AppDbContext c) => _context = c;

        public async Task<Item> GetItemByIdAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);

            _context.Entry(item).Reference(m => m.From).Load();

            return item;
        }

        public List<Item> GetItems()
        {
            return _context.Items
            .Include(m => m.From)
            .OrderBy(m => m.ItemId)
            .ToList();
        }

        public async Task<int> StoreItemAsync(Item item)
        {
            await _context.AddAsync(item);

            return _context.SaveChanges();
        }

        public int UpdateItem(Item item)
        {
            _context.Update(item);

            return _context.SaveChanges();
        }

        public int DeleteItem(int itemId)
        {
            Item item = GetItemByIdAsync(itemId).Result;

            _context.Items.Remove(item);

            return _context.SaveChanges();
        }
    }
}
