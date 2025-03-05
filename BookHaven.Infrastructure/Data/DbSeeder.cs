using BookHaven.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookHaven.Infrastructure.Data
{
    public class DbSeeder
    {
        public static async Task SeedData(UserManager<ApplicationUser>? userManager, RoleManager<IdentityRole>? roleManager)
        {
            if (userManager == null || roleManager == null)
            {
                return;
            }

            bool adminRoleExists = await roleManager.RoleExistsAsync("admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            bool userRoleExists = await roleManager.RoleExistsAsync("user");
            if (!userRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            // Add default admin
            var admins = await userManager.GetUsersInRoleAsync("admin");
            if (admins.Any())
            {
                return;
            }

            var admin = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "BookHaven",
                UserName = "admin@bookhaven.com",
                Email = "admin@bookhaven.com"
            };

            string password = "admin123";

            var result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
                Console.WriteLine("Admin created successfully");
            }
        }
    }
}
