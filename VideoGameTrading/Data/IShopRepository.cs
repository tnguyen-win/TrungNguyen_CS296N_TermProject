using VideoGameTrading.Models;

namespace VideoGameTrading.Data
{
    public interface IShopRepository
    {
        public List<Item> GetItems();

        public Task<Item> GetItemByIdAsync(int id);

        public Task<int> StoreItemAsync(Item item);

        public int UpdateItem(Item item);

        public int DeleteItem(int itemId);
    }
}
