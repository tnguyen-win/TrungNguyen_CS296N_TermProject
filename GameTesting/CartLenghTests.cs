using VideoGameTrading.Models;

namespace GameTesting {
    public class CartLengthTests {
        [Fact]
        public void CreateCartLength() {
            var cartlength = new CartLength() {
                CartTotal = 14
            };

            Assert.True(cartlength.CartTotal == 14);
        }
    }
}
