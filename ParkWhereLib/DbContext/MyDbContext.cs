using Microsoft.EntityFrameworkCore;
using ParkWhereLib.Models; // Ensure namespace matches your file structure

namespace ParkWhereLib.Models
{
    public partial class MyDbContext : DbContext
    {
        // 1. Keep this constructor. It is the standard for EF Core 
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        // 2. Remove the constructor that takes IConfiguration.
        // 3. Remove the private IConfiguration field.

        public DbSet<Car> Cars { get; set; }
        public DbSet<ParkingEvent> ParkingEvents { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }

        // 4. Remove OnConfiguring. 
        // Logic in Program.cs (builder.Services.AddDbContext...) handles the connection string.
        // If you keep OnConfiguring, make sure it checks if options are already configured.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Only used if you create the Context manually without DI, 
                // typically generally safer to remove this method entirely 
                // to avoid double-configuration errors.
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            // This tells EF Core: "If ID 1 doesn't exist, create it automatically."
            modelBuilder.Entity<ParkingLot>().HasData(
                new ParkingLot { ParkingLotId = 1, CarsParked = 0 }
            );
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}