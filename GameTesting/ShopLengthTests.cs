using VideoGameTrading.Models;

namespace GameTesting {
    public class ShopLengthTests {
        [Fact]
        public void CreateShopLength() {
            var shoplength = new ShopLength() {
                ShopTotal = 7
            };

            Assert.True(shoplength.ShopTotal == 7);
        }
    }
}
