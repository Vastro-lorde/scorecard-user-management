using Microsoft.AspNetCore.Identity;
using scorecard_user_mgt.Data;
using scorecard_user_mgt.Models;
using scorecard_user_mgt.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scorecard_user_mgt
{
    public class Seeders
    {
        public class Seeder
        {
            public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, AppDbContext dbContext)
            {
                await dbContext.Database.EnsureCreatedAsync();

                await CreateUserRolesAsync(roleManager);
                await SeedUser(userManager, dbContext);
            }

            private static async Task CreateUserRolesAsync(RoleManager<IdentityRole> roleManager)
            {
                var userRoles = new List<IdentityRole>
            {
                new IdentityRole(UserRoles.Admin.ToString()),
                new IdentityRole(UserRoles.Dev.ToString()),
                new IdentityRole(UserRoles.PA.ToString()),
            };
                if (!roleManager.Roles.Any())
                {
                    foreach (IdentityRole role in userRoles)
                    {
                        var result = await roleManager.CreateAsync(role);
                    }
                }
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