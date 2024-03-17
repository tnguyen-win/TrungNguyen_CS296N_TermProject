using VideoGameTrading.Models;

namespace VideoGameTrading.Data
{
    public class FakeCartLengthRepository : ICartLengthRepository
    {
        readonly List<CartLength> _shoplength = new();

        public async Task<CartLength> GetCartLengthByIdAsync(int id) => throw new NotImplementedException();

        public List<CartLength> GetCartLength() => throw new NotImplementedException();

        public async Task<int> StoreCartLengthAsync(CartLength shoplength)
        {
            shoplength.CartLengthId = _shoplength.Count + 1;

            _shoplength.Add(shoplength);

            return shoplength.CartLengthId;
        }
    }
}
