using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ParkWhereLib.Models;

public partial class MyDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public MyDbContext()
    {
    }
    public MyDbContext(DbContextOptions<MyDbContext> options, IConfiguration configuration)
            : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Car> Cars { get; set; }

    public DbSet<ParkingEvent> ParkingEvents { get; set; }

    public DbSet<ParkingLot> ParkingLots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
