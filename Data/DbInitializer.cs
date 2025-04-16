using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogProject.Models;
using Bogus;

namespace BlogProject.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            // Roller
            var roles = new[] { "Admin", "Kullanıcı" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Admin kullanıcı
            var adminEmail = "admin@site.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    AdSoyad = "Yönetici Admin"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123*");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Default kategoriler
            if (!context.Kategoriler.Any())
            {
                var kategoriler = new List<Kategori>
                {
                    new Kategori { Ad = "Genel" },
                    new Kategori { Ad = "Yazılım" },
                    new Kategori { Ad = "Tasarım" },
                    new Kategori { Ad = "Veri Bilimi" },
                    new Kategori { Ad = "Teknoloji" },
                };

                context.Kategoriler.AddRange(kategoriler);
                await context.SaveChangesAsync();
            }

            // Test kullanıcı (faker ile)
            AppUser fakeUser;
            var testUserEmail = "test@user.com";
            fakeUser = await userManager.FindByEmailAsync(testUserEmail);

            if (fakeUser == null)
            {
                fakeUser = new AppUser
                {
                    UserName = "testuser",
                    Email = testUserEmail,
                    EmailConfirmed = true,
                    AdSoyad = "Test Kullanıcı"
                };

                var result = await userManager.CreateAsync(fakeUser, "Test123*");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(fakeUser, "Kullanıcı");
                }
            }


            var uploadsFolderPath = "wwwroot/uploads";
            var imageFiles = Directory.GetFiles(uploadsFolderPath, "*.*")
                .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png") || f.EndsWith(".jpeg"))
                .ToList();

            // Fake Postlar
            if (!context.Posts.Any())
            {
                var kategoriler = context.Kategoriler.ToList();
                var random = new Random();

                var faker = new Faker<Post>()
                    .RuleFor(p => p.Baslik, f => f.Lorem.Sentence(5))
                    .RuleFor(p => p.Icerik, f => f.Lorem.Paragraphs(3))
                    .RuleFor(p => p.OlusturmaTarihi, f => f.Date.Past(1))
                    .RuleFor(p => p.KategoriId, f => kategoriler[random.Next(kategoriler.Count)].Id)
                    .RuleFor(p => p.AppUserId, fakeUser.Id)
                    .RuleFor(p => p.GorselYolu, f => imageFiles.Count > 0
                        ? "/uploads/" + Path.GetFileName(imageFiles[random.Next(imageFiles.Count)])
                        : "/uploads/sample.jpg")
                    .RuleFor(p => p.IsDeleted, false);

                var fakePosts = faker.Generate(150);
                await context.Posts.AddRangeAsync(fakePosts);
                await context.SaveChangesAsync();
            }
        }
    }
}
