using Identity;
using Identity.Models;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Real_estate.API.IntegrationTests.Controllers;
using Real_estate.Domain.Entities;
using static Real_estate.Domain.Enums.Enums;



namespace Real_estate.API.IntegrationTests.Base
{
    public class Seed
    {

        public static void InitializeDbForTests(RealEstateContext context)
        {
            var categories = new List<Listing>
                {
                 Listing.Create("Apartament",100.50m, "user","apartament1","apartament 2 camere",Status.ForRent).Value,
                 Listing.Create("House",100.50m, "user","house1","casa 4 camere",Status.ForSale).Value,
                 Listing.Create("Apartament2",100.50m, "user","apartament2","apartament 3 camere",Status.ForSale).Value,
                 Listing.Create("House2",100.50m, "user","house2","casa 4 camere",Status.ForRent).Value


            };
            context.Listings.AddRange(categories);
            context.SaveChanges();

            var properties = new List<Property>
            {
                 Property.Create("Apartment", "123 Main St", 120, 300000, "user1", 2).Value,
                 Property.Create("House", "456 Elm St", 200, 500000, "user2", 4).Value,
                 Property.Create("Studio", "789 Oak St", 80, 150000, "user3", 1).Value,
                 Property.Create("Villa", "101 Pine St", 250, 750000, "user4", 5).Value
            };
            context.Properties.AddRange(properties);
            context.SaveChanges();

        }
        public static async Task InitializeDbForTests(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "BlueSkyWalker", Email = "lucas.turner@example.com", Name = "Lucas Turner" },
                new ApplicationUser { UserName = "TechSavvy21", Email = "sarah.johnson@example.com", Name = "Sarah Johnson" },
                new ApplicationUser { UserName = "GreenThumb88", Email = "emily.clark@example.com", Name = "Emily Clark" },
                new ApplicationUser { UserName = "MountainExplorer", Email = "alex.smith@example.com", Name = "Alex Smith" }
             };

            foreach (var user in users)
            {
                if (await userManager.FindByNameAsync(user.UserName) == null)
                {
                    await userManager.CreateAsync(user, "DefaultP@ssword123!"); // Use a default password for testing
                                                                                // Optionally assign roles here if needed
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
