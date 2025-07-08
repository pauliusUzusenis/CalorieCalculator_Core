using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CalorieCalculator.Models;
using CalorieCalculatorCore.Extensions.Microsoft.EntityFrameworkCore;

namespace CalorieCalculatorCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<MeasureType> MeasureTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeasureType>().HasData(
                new MeasureType
                {
                    Id = 1,
                    Name = "Gram",
                    Uri = "http://www.edamam.com/ontologies/edamam.owl#Measure_gram",
                    Symbol = "g",
                },
                new MeasureType
                {
                    Id = 2,
                    Name = "Milliliter",
                    Uri = "http://www.edamam.com/ontologies/edamam.owl#Measure_milliliter",
                    Symbol = "ml",
                }
            );
            modelBuilder.RemovePluralizingTableNameConvention();
            base.OnModelCreating(modelBuilder);
        }
    }
}
