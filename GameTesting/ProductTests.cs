using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using VideoGameTrading.Controllers;
using VideoGameTrading.Data;
using VideoGameTrading.Models;

namespace GameTesting {
    public class GameTesting {
        [Fact]
        public void ProductItemTest() {
            var repo = new FakeProductRepository();
            var controller = new ShopController(repo);
            var model = new Item() {
                InCart = false,
                Title = "This is a test",
                Genre = "This is a test",
                ReleaseYear = 2023,
                AgeRange = "This is a test",
                Condition = "This is a test",
                From = new AppUser { Name = "Tester", }
            };

            controller.Create(model);

            Assert.True(model.ItemId >= 0);
            Assert.False(model.InCart);
            Assert.InRange(model.Price, 0, 100);
        }
    }
}
