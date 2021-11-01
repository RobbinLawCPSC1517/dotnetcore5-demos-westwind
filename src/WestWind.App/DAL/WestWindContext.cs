using Microsoft.EntityFrameworkCore;
using WestWind.Entities;

namespace WestWind.DAL {
//DAL is short for Data Access Layer
    public class WestWindContext : DbContext {
        //constructor dependancy injection
        //inject the options which will tell the database context class where to access the database
        public WestWindContext(DbContextOptions<WestWindContext> options)
            : base(options) {}
        public DbSet<BuildVersion> BuildVersion { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}