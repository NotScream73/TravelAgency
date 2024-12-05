using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency.Data;

public class DataContext : DbContext
{
    public DbSet<Country> Country { get; set; }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().ToTable("country").HasKey(i => i.Id);
        modelBuilder.Entity<Country>().Property(i => i.Id).UseIdentityColumn();
    }
}
