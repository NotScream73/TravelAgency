using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Country> Country { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().ToTable("country").HasKey(i => i.Id);
        modelBuilder.Entity<Country>().Property(i => i.Id).UseIdentityColumn();
    }
}
