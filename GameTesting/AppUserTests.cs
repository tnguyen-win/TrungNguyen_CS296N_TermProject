using VideoGameTrading.Models;

namespace GameTesting {
    public class AppUserTests {
        [Fact]
        public void CreateAppUser() {
            var appuser = new AppUser() {
                Name = "John Doe"
            };

            Assert.True(appuser.Name != "");
        }
    }
}
