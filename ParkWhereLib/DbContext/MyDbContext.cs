using Microsoft.EntityFrameworkCore;
using ParkWhereLib.Models; 

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

            modelBuilder.Entity<ParkingLot>().HasData(
                new ParkingLot { ParkingLotId = 1, CarsParked = 0 }
            );
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}