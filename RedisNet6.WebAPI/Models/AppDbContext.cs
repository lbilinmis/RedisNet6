using Microsoft.EntityFrameworkCore;

namespace RedisNet6.WebAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(

                new Product() { Id = 11, Name = "Ürün 1", Price = 1000 },
                new Product() { Id = 22, Name = "Ürün 2", Price = 2000 },
                new Product() { Id = 13, Name = "Ürün 3", Price = 3000 },
                new Product() { Id = 14, Name = "Ürün 4", Price = 4000 },
                new Product() { Id = 15, Name = "Ürün 5", Price = 5000 },
                new Product() { Id = 16, Name = "Ürün 6", Price = 6000 },
                new Product() { Id = 17, Name = "Ürün 7", Price = 7000 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
