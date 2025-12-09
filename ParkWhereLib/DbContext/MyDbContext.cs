using Microsoft.EntityFrameworkCore;
using ParkWhereLib.Models; // Ensure namespace matches your file structure

namespace ParkWhereLib.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<ParkingEvent> ParkingEvents { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }

     
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