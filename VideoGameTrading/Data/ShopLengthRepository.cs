using VideoGameTrading.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameTrading.Data
{
    public class ShopLengthRepository : IShopLengthRepository
    {
        readonly AppDbContext _context;

        public ShopLengthRepository(AppDbContext c) => _context = c;

        public async Task<ShopLength> GetShopLengthByIdAsync(int id)
        {
            var shoplength = await _context.ShopLength.FindAsync(id);

            return shoplength;
        }

        public List<ShopLength> GetShopLength()
        {
            return _context.ShopLength
            .Include(m => m.ShopLengthId)
            .ToList();
        }

        public async Task<int> StoreShopLengthAsync(ShopLength shoplength)
        {
            await _context.AddAsync(shoplength);

            return _context.SaveChanges();
        }
    }
}
