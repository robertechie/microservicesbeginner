using Microsoft.EntityFrameworkCore;
using CustomersApi.Models;

namespace CustomersApi.Data;

public class AppDbContext:DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Vehicle?> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost,1433;database=CustomerApiDb;User=SA;Password=MyPass@word");
    }
}