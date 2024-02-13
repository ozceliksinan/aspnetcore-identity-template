using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "sinanozcelik";
        private const string adminPassword = "Sinan123.";
        private const string adminFullName = "Sinan Özçelik";
        private const string adminEmailAddress = "info@sinanozcelik.com";
        private const string adminPhoneNumber = "05555555555";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();

            if (context.Database.GetAppliedMigrations().Any())
            {
                // Bekleyen Migration islemi var mi ? //
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            // admin rolu var mi? yoksa olustur.
            var adminRole = await roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "admin" });
            }

            // Eklenecek olan kullanici var mi ? //
            var user = await userManager.FindByNameAsync(adminUser);

            // Eger kullanici yoksa yeni user olusturulsun //
            if (user == null)
            {
                user = new AppUser
                {
                    FullName = adminFullName,
                    UserName = adminUser,
                    Email = adminEmailAddress,
                    PhoneNumber = adminPhoneNumber,
                    EmailConfirmed = true
                };

                // Yeni kullanici olusturulsun. Parola verilirse login yetkisi olur. //
                await userManager.CreateAsync(user, adminPassword);
                // Yeni kullanici otomatik olarak admin rolunde olsun.
                await userManager.AddToRoleAsync(user, "admin");
            }
        }
    }
}