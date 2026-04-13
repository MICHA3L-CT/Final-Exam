using E_commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Data
{
    public class SeedData
    {
        public static async Task SeedUsersAndRoles(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seeded my roles
            string[] roleNames = { "Admin", "Producer", "Customer", "Developer" };
            foreach (string roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }

            // Seeding users and assigning roles, one for each type of user for now
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(adminUser, "Password123!");
            }
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Adding the 3 producer users for different producers
            var producerUser = await userManager.FindByEmailAsync("producer@example.com");
            if (producerUser == null)
            {
                producerUser = new IdentityUser { UserName = "producer@example.com", Email = "producer@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(producerUser, "Password123!");
            }
            if (!await userManager.IsInRoleAsync(producerUser, "Producer"))
            {
                await userManager.AddToRoleAsync(producerUser, "Producer");
            }

            var producerUser2 = await userManager.FindByEmailAsync("producer2@example.com");
            if (producerUser2 == null)
            {
                producerUser2 = new IdentityUser { UserName = "producer2@example.com", Email = "producer2@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(producerUser2, "Password123!");
            }
            if (!await userManager.IsInRoleAsync(producerUser2, "Producer"))
            {
                await userManager.AddToRoleAsync(producerUser2, "Producer");
            }

            var producerUser3 = await userManager.FindByEmailAsync("producer3@example.com");
            if (producerUser3 == null)
            {
                producerUser3 = new IdentityUser { UserName = "producer3@example.com", Email = "producer3@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(producerUser3, "Password123!");
            }
            if (!await userManager.IsInRoleAsync(producerUser3, "Producer"))
            {
                await userManager.AddToRoleAsync(producerUser3, "Producer");
            }

            // Adding the normal and dev user
            var devUser = await userManager.FindByEmailAsync("dev@example.com");
            if (devUser == null)
            {
                devUser = new IdentityUser { UserName = "dev@example.com", Email = "dev@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(devUser, "Password123!");
            }
            if (!await userManager.IsInRoleAsync(devUser, "Developer"))
            {
                await userManager.AddToRoleAsync(devUser, "Developer");
            }

            var normalUser = await userManager.FindByEmailAsync("user@example.com");
            if (normalUser == null)
            {
                normalUser = new IdentityUser { UserName = "user@example.com", Email = "user@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(normalUser, "Password123!");
            }
            if (!await userManager.IsInRoleAsync(normalUser, "Customer"))
            {
                await userManager.AddToRoleAsync(normalUser, "Customer");
            }
        }

        public static async Task SeedProducers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var producerUser1 = await userManager.FindByEmailAsync("producer@example.com");
            var producerUser2 = await userManager.FindByEmailAsync("producer2@example.com");
            var producerUser3 = await userManager.FindByEmailAsync("producer3@example.com");

            if (producerUser1 == null || producerUser2 == null || producerUser3 == null)
            {
                throw new Exception("Producer users not found.");
            }

            if (context.Producer.Any())
                return; // Producers already seeded

            var producers = new List<Producer>
            {
                new Producer
                {
                    ProducerName = "Daniel's Farmfoods",
                    PhoneNumber = "07700900001",
                    ProductDescription = "We grow a wide variety of seasonal fruits and vegetables using traditional organic methods. Our produce is harvested fresh  and delivered straight to customers, ensuring maximum freshness and nutritional value. Specialities include heritage tomatoes, mixed salad leaves, root vegetables and a rotating selection of seasonal soft fruits as well as an assortment of meat and fish.",
                    Location = "Birmingham",
                    ProducerInfo = "Daniel's Farm has been family-owned and operated for over 30 years in the heart of the West Midlands. We are fully certified organic and take great pride in sustainable, low-carbon farming practices. Every product is grown without pesticides or artificial fertilisers, and we work closely with local schools and community groups to promote healthy eating.",
                    DateJoined = new DateOnly(2023, 4, 10),
                    IsVerified = true,
                    UserId = producerUser1.Id
                },
                new Producer
                {
                    ProducerName = "Green Valley Gardens",
                    PhoneNumber = "07700900002",
                    ProductDescription = "Green Valley Gardens specialises in freshly cut herbs, edible flowers and premium salad produce. Our growing tunnels allow us to supply high-quality greens year-round regardless of season. We also produce a range of artisan jams, chutneys and preserves made from surplus harvest using traditional recipes.",
                    Location = "Coventry",
                    ProducerInfo = "Green Valley Gardens was established in 2018 by Sarah Greenwood, a former chef turned passionate grower. Sarah brings her culinary expertise directly to her growing decisions, focusing on flavour and texture above all else. The farm operates on a zero-waste principle — anything not sold fresh is preserved, pickled or composted.",
                    DateJoined = new DateOnly(2023, 7, 22),
                    IsVerified = true,
                    UserId = producerUser2.Id
                },
                new Producer
                {
                    ProducerName = "Sunny Fields Farm",
                    PhoneNumber = "07700900003",
                    ProductDescription = "Sunny Fields Farm produces free-range eggs, seasonal root vegetables and a popular range of freshly baked goods including sourdough loaves, wholegrain rolls and homemade pies. All baked products are made on-site using our own milled flour and farm-fresh eggs, with no artificial additives or preservatives.",
                    Location = "Wolverhampton",
                    ProducerInfo = "Sunny Fields Farm has been in the Holloway family for three generations. James took over the farm in 2015 and expanded the operation to include a small bakery and free-range poultry unit. The farm is committed to animal welfare and environmental sustainability, running entirely on solar energy since 2021. James is a strong advocate for supporting local food networks and reducing dependence on supermarket supply chains.",
                    DateJoined = new DateOnly(2024, 1, 5),
                    IsVerified = true,
                    UserId = producerUser3.Id
                }
            };

            await context.Producer.AddRangeAsync(producers);
            await context.SaveChangesAsync();
        }

        public static async Task SeedProducts(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var danielsFarmfoods = await context.Producer.FirstOrDefaultAsync(p => p.ProducerName == "Daniel's Farmfoods");
            var greenValleyGardens = await context.Producer.FirstOrDefaultAsync(p => p.ProducerName == "Green Valley Gardens");
            var sunnyFieldsFarm = await context.Producer.FirstOrDefaultAsync(p => p.ProducerName == "Sunny Fields Farm");

            if (danielsFarmfoods == null || greenValleyGardens == null || sunnyFieldsFarm == null)
            {
                throw new Exception("Producer not found.");
            }
        }

    }
}