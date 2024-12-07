using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency.Data;

public class DataContext : DbContext
{
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Resort> Resorts { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().ToTable("country").HasKey(i => i.Id);
        modelBuilder.Entity<Country>().Property(i => i.Id).UseIdentityColumn();

        modelBuilder.Entity<Resort>().ToTable("resort").HasKey(i => i.Id);
        modelBuilder.Entity<Resort>().Property(i => i.Id).UseIdentityColumn();
        modelBuilder.Entity<Resort>().HasIndex(i => i.Name).IsUnique();
    }
}
