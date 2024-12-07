using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency.Data;

public class DataContext : IdentityDbContext<User>
{
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Resort> Resorts { get; set; } = null!;
    public DbSet<Accommodation> Accommodations { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Country>().ToTable("country").HasKey(i => i.Id);
        builder.Entity<Country>().Property(i => i.Id).UseIdentityColumn();
        builder.Entity<Country>().HasIndex(i => i.Name).IsUnique();

        builder.Entity<Resort>().ToTable("resort").HasKey(i => i.Id);
        builder.Entity<Resort>().Property(i => i.Id).UseIdentityColumn();
        builder.Entity<Resort>().HasIndex(i => i.Name).IsUnique();

        builder.Entity<Accommodation>().ToTable("accommodation").HasKey(i => i.Id);
        builder.Entity<Accommodation>().Property(i => i.Id).UseIdentityColumn();
        builder.Entity<Accommodation>().HasIndex(i => i.Name).IsUnique();
    }
}
