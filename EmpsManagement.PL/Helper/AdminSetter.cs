using EmpsManagement.DAL.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace EmpsManagement.PL.Helper
{
    public static class AdminSetter
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var adminRole = "Admin";
            var adminEmail = "ahmedsamir.as9728@gmail.com";
            var adminPassword = "Ahmed**1099";
            var adminUserFName = "Ahme";
            var adminUserLName = "Samir";

            // Create role if it doesn't exist
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Create user if it doesn't exist
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = adminUserFName,
                    LastName = adminUserLName,
                    UserName = adminEmail.Split('@')[0],
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }
        }
    }
}
