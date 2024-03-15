using VideoGameTrading.Controllers;
using VideoGameTrading.Data;
using VideoGameTrading.Models;

namespace GameTesting {
    public class GameTesting {
        [Fact]
        public void StoreItemTests() {
            var repo = new FakeShopRepository();
            var controller = new ShopController(repo, null!);
            var model = new Item() {
                Title = "This is a test",
                Genre = "This is a test",
                ReleaseYear = 2023,
                AgeRange = "This is a test",
                Condition = "This is a test",
                From = new AppUser { Name = "Tester", }
            };

            controller.Create(model).Wait();

            Assert.True(model.ImageId > 0);
            Assert.True(model.ItemId > 0);
            Assert.False(model.InCart);
            Assert.True(model.Title != "");
            Assert.True(model.Genre != "");
            Assert.True(model.ReleaseYear >= 1958);
            Assert.InRange(model.Price, 0, 100);
            Assert.True(model.AgeRange != "");
            Assert.True(model.Condition != "");
            Assert.True(model.From != null);
        }
    }
}
