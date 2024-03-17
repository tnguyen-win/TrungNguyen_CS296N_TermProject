using VideoGameTrading.Models;

namespace VideoGameTrading.Data
{
    public interface ICartLengthRepository
    {
        public Task<CartLength> GetCartLengthByIdAsync(int id);

        public List<CartLength> GetCartLength();

        public Task<int> StoreCartLengthAsync(CartLength shoplength);
    }
}
