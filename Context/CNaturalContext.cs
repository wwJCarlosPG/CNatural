using Microsoft.EntityFrameworkCore;
using CNaturalApi.Models;


namespace CNaturalApi.Context
{
    public class CNaturalContext:DbContext
    {
        public CNaturalContext(DbContextOptions<CNaturalContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 2, Investments = null, Sales = null, Name = "Samsung Galaxy A12" },
                new Product { Id = 3, Investments = null, Sales = null, Name = "OnePlus Nord 2" },
                new Product { Id = 4, Investments = null, Sales = null, Name = "Oppo Find X5 Pro" },
                new Product { Id = 5, Investments = null, Sales = null, Name = "iPhone 12 Pro Max" });
            modelBuilder.Entity<Buyer>().HasData(
                new Buyer { Id = 1, IsDeleted = false, Mobile = "54401444", Address = "Calle 28", Name = "Juan", Sales = null },
                new Buyer { Id = 2, IsDeleted = false, Mobile = "53464918", Address = "Calle 23", Name = "Luis", Sales = null },
                new Buyer { Id = 3, IsDeleted = false, Mobile = "52269101", Address = "Calle 26", Name = "Maria", Sales = null });
            modelBuilder.Entity<Accountancy>().HasData(
                new Accountancy { Id = 1, Sales = null, Investments = null, Date = DateTime.Now, EarnedMoney = 0, InvestedMoney = 0 });

        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null !;
        public DbSet<Buyer> Buyers { get; set; } = null!;
        public DbSet<Investment> Investments { get; set; } = null!;
        public DbSet<Accountancy> Accountancies { get; set; } = null!;   //Esta tiene cada mes de contabilidad, se agrega un nuevo elemento el dia 1 de cada mes.

    }
}
