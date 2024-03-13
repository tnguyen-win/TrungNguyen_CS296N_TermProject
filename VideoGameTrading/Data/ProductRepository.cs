using VideoGameTrading.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameTrading.Data {
    public class ProductRepository : IProductRepository {
        readonly AppDbContext context;
        public ProductRepository(AppDbContext c) => context = c;

        public Item GetItemById(int id) => throw new NotImplementedException();

        public List<Item> GetItems() {
            return context.Items

            .Include(m => m.From)
            .OrderBy(m => m.ItemId)
            .ToList();
        }

        public int StoreItem(Item item) {
            context.Add(item);

            return context.SaveChanges();
        }
    }
}
