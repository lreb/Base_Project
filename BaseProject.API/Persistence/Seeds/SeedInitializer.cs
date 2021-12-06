using Microsoft.EntityFrameworkCore;

namespace BaseProject.API.Persistence.Seeds
{
    public static class SeedInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Item>().HasData(ItemSeed.ItemList());
        }
    }
}
