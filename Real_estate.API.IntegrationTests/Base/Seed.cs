using Infrastructure;
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
        }
    }
}
