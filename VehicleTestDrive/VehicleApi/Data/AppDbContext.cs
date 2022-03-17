using Microsoft.EntityFrameworkCore;
using VehicleApi.Models;

namespace VehicleApi.Data;

public class AppDbContext:DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost,1433;database=VehicleApiDb;User=SA;Password=MyPass@word");
    }
}