using Domain;
using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task InitializeAsync(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager)
    {
        string[] roles = { "Admin", "User", "Employer", "Developer" };

        // ایجاد نقش‌ها اگر وجود ندارند
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new AppRole(role));
        }

        // ایجاد کاربر ادمین
        string adminEmail = "admin@example.com";
        string adminPassword = "Admin123$";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new AppUser()
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
