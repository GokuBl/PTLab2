using Microsoft.EntityFrameworkCore;
using PTLab2_testASP.Models;

namespace PTLab2_testASP.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        public DbSet<Product> Products {get; set;}
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ID = 1, Name = "Black Sofa", Price = 15200 },
                new Product { ID = 2, Name = "Blue Sofa", Price = 20000 },
                new Product { ID = 3, Name = "Red Sofa", Price = 25000 }
                );
        }
    }
}
