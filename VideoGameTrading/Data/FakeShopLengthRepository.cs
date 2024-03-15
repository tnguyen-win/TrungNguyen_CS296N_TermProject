using VideoGameTrading.Models;

namespace VideoGameTrading.Data
{
    public class FakeShopLengthRepository : IShopLengthRepository
    {
        readonly List<ShopLength> _shoplength = new();

        public async Task<ShopLength> GetShopLengthByIdAsync(int id) => throw new NotImplementedException();

        public List<ShopLength> GetShopLength() => throw new NotImplementedException();

        public async Task<int> StoreShopLengthAsync(ShopLength shoplength)
        {
            shoplength.ShopLengthId = _shoplength.Count + 1;

            _shoplength.Add(shoplength);

            return shoplength.ShopLengthId;
        }
    }
}
