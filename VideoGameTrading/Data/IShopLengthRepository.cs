using VideoGameTrading.Models;

namespace VideoGameTrading.Data
{
    public interface IShopLengthRepository
    {
        public Task<ShopLength> GetShopLengthByIdAsync(int id);

        public List<ShopLength> GetShopLength();

        public Task<int> StoreShopLengthAsync(ShopLength shoplength);
    }
}
