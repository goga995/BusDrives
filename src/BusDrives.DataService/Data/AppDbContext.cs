using BusDrives.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace BusDrives.DataService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<BusDrive> BusDrives { get; set; }
    public DbSet<City> Cities { get; set; }
}