using VideoGameTrading.Data;
using VideoGameTrading.Models;

namespace VideoGameTrading {
    public class SeedData {
        public static void Seed(AppDbContext context) {
            if (!context.Items.Any()) {
                var user1 = new AppUser { Name = "Brian" };
                var user2 = new AppUser { Name = "Amanda" };

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.SaveChanges();

                Random rnd1 = new();
                Random rnd2 = new();

                var item1 = new Item {
                    From = user2,
                    InCart = false,
                    Title = "A test title",
                    Genre = "Strategy",
                    ReleaseYear = 2001,
                    Price = Math.Round((double)(1 + (rnd1.NextDouble() * (100 - 1))), 2),
                    AgeRange = "Everyone",
                    Condition = "New"
                };

                context.Items.Add(item1);

                var item2 = new Item {
                    From = user1,
                    Title = "Another test title",
                    Genre = "RPG",
                    ReleaseYear = 2017,
                    Price = Math.Round((double)(1 + (rnd2.NextDouble() * (100 - 1))), 2),
                    AgeRange = "Everyone +10",
                    Condition = "Excellent"
                };

                context.Items.Add(item2);
                context.SaveChanges();
            }
        }
    }
}
