using Microsoft.EntityFrameworkCore;
using WestWind.Entities;

namespace WestWind.DAL {
//DAL is short for Data Access Layer
    public class TrainWatchContext : DbContext {
        //constructor
        //inject the options which will tell the database context class where to access the database
        public TrainWatchContext(DbContextOptions<TrainWatchContext> options)
            : base(options) {}
        public DbSet<DbVersion> DbVersion { get; set; }
        public DbSet<RailCarType> RailCarTypes { get; set; }
        public DbSet<RollingStock> RollingStock { get; set; }
    }
}