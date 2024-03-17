using VideoGameTrading.Models;

namespace VideoGameTrading.Data
{
    public class FakeShopRepository : IShopRepository
    {
        readonly List<Item> _items = new();

        public async Task<Item> GetItemByIdAsync(int id) => throw new NotImplementedException();

        public List<Item> GetItems() => throw new NotImplementedException();

        public async Task<int> StoreItemAsync(Item item)
        {
            item.ItemId = _items.Count + 1;

            _items.Add(item);

            return item.ItemId;
        }

        public int UpdateItem(Item item) => throw new NotImplementedException();

        public int DeleteItem(int itemId) => throw new NotImplementedException();
    }
}
