using VideoGameTrading.Models;

namespace VideoGameTrading.Data {
    public class FakeProductRepository : IProductRepository {
        readonly List<Item> items = new();

        public Item GetItemById(int id) => throw new NotImplementedException();

        public List<Item> GetItems() => throw new NotImplementedException();

        public int StoreItem(Item item) {
            items.Add(item);

            return item.ItemId;
        }
    }
}
