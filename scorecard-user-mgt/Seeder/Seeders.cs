using Microsoft.AspNetCore.Identity;
using scorecard_user_mgt.Data;
using scorecard_user_mgt.Models;
using System.Linq;
using System.Threading.Tasks;

namespace scorecard_user_mgt
{
    public class Seeders
    {
        public class Seeder
        {
            public async static Task Seed( UserManager<User> userManager, AppDbContext dbContext)
            {
                await dbContext.Database.EnsureCreatedAsync();
                await SeedUser(userManager, dbContext);
            }
            private static async Task SeedUser(UserManager<User> userManager, AppDbContext dbContext)
            {
                if (!dbContext.Users.Any())
                {
                    var users = SeederHelper<User>.GetData("User.json");
                    foreach (var user in users)
                    {
                        await userManager.CreateAsync(user, "Ja@125");
                    }
                }
            }
        }
    }
}