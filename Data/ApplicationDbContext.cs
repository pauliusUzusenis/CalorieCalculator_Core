using CalorieCalculator.Models;
using CalorieCalculatorCore.Extensions.Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CalorieCalculatorCore.Data
{
    public class ApplicationDbContext : DbContext
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
            modelBuilder.RemovePluralizingTableNameConvention();
        }
    }
}
