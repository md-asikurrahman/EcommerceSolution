using ECommerceSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSolution.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        
        public DbSet<Address> tblAddress { get; set; }
        public DbSet<AddressType> tblAddressTypes { get; set; }
        public DbSet<Brand> tblBrands { get; set; }
        public DbSet<Category> tblCategories { get; set; }
        public DbSet<Designation> tblDesignations { get; set; }
        public DbSet<Employee> tblEmployees { get; set; }
        public DbSet<Product> tblProducts { get; set; }
        public DbSet<Customer> tblUsers { get; set; }
        public DbSet<Vendor> tblVendors { get; set; }
        public DbSet<VendorType> tblVendorTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Hardcoded connection string
            optionsBuilder.UseSqlServer("Server=DESKTOP-4AQFFCU;Database=Ecommerce_Db;User=sa;Password=123456;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
    }
}
