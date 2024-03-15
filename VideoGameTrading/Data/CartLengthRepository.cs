using VideoGameTrading.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameTrading.Data
{
    public class CartLengthRepository : ICartLengthRepository
    {
        readonly AppDbContext _context;

        public CartLengthRepository(AppDbContext c) => _context = c;

        public async Task<CartLength> GetCartLengthByIdAsync(int id)
        {
            var shoplength = await _context.CartLength.FindAsync(id);

            return shoplength;
        }

        public List<CartLength> GetCartLength()
        {
            return _context.CartLength
            .Include(m => m.CartLengthId)
            .ToList();
        }

        public async Task<int> StoreCartLengthAsync(CartLength shoplength)
        {
            await _context.AddAsync(shoplength);

            return _context.SaveChanges();
        }
    }
}
