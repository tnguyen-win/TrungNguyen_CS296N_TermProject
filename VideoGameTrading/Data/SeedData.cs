using VideoGameTrading.Data;
using VideoGameTrading.Models;
using Microsoft.AspNetCore.Identity;

namespace VideoGameTrading
{
    public class SeedData
    {
        public static void Seed(AppDbContext context, IServiceProvider provider)
        {
            if (context.Items != null && !context.Items.Any())
            {
                Random rnd1 = new();
                Random rnd2 = new();

                var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                const string ROLE = "Admin";
                const string SECRET_PASSWORD = "Secret!123";
                bool isSuccess = true;

                if (roleManager.FindByNameAsync(ROLE).Result == null) isSuccess = roleManager.CreateAsync(new IdentityRole(ROLE)).Result.Succeeded;

                var user1 = new AppUser { Name = "John Smith", UserName = "John" };
                var user2 = new AppUser { Name = "Jane Doe", UserName = "Jane" };

                isSuccess &= userManager.CreateAsync(user1, SECRET_PASSWORD).Result.Succeeded;

                if (isSuccess) isSuccess &= userManager.AddToRoleAsync(user1, ROLE).Result.Succeeded;

                isSuccess &= userManager.CreateAsync(user2, SECRET_PASSWORD).Result.Succeeded;

                if (isSuccess)
                {
                    var item1 = new Item
                    {
                        ImageId = 2,
                        InCart = false,
                        Title = "A test title",
                        Genre = "Strategy",
                        ReleaseYear = 2001,
                        Price = Math.Round((double)(1 + (rnd1.NextDouble() * (100 - 1))), 2),
                        AgeRange = "Everyone",
                        Condition = "New",
                        From = user1
                    };

                    context.Items.Add(item1);

                    var item2 = new Item
                    {
                        ImageId = 7,
                        Title = "Another test title",
                        Genre = "RPG",
                        ReleaseYear = 2017,
                        Price = Math.Round((double)(1 + (rnd2.NextDouble() * (100 - 1))), 2),
                        AgeRange = "Everyone +10",
                        Condition = "Excellent",
                        From = user2
                    };

                    context.Items.Add(item2);
                    context.SaveChanges();
                }
            }
        }
    }
}
