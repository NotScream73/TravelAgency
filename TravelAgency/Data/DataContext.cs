using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency.Data;

public class DataContext : IdentityDbContext<User>
{
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Resort> Resorts { get; set; } = null!;
    public DbSet<Accommodation> Accommodations { get; set; } = null!;
    public DbSet<Tour> Tours { get; set; } = null!;
    public DbSet<Purchase> Purchases { get; set; } = null!;
    public DbSet<TourPurchase> TourPurchases { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

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

        builder.Entity<Tour>().ToTable("tour").HasKey(i => i.Id);
        builder.Entity<Tour>().Property(i => i.Id).UseIdentityColumn();
        builder.Entity<Tour>().HasIndex(i => i.Name).IsUnique();

        builder.Entity<Tour>()
            .HasOne(t => t.Country)
            .WithMany()
            .HasForeignKey(t => t.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Tour>()
            .HasOne(t => t.Accommodation)
            .WithMany()
            .HasForeignKey(t => t.AccommodationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Tour>()
            .HasOne(t => t.Resort)
            .WithMany()
            .HasForeignKey(t => t.ResortId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Purchase>().ToTable("purchase").HasKey(i => i.Id);
        builder.Entity<Purchase>().Property(i => i.Id).UseIdentityColumn();

        builder.Entity<TourPurchase>().ToTable("tour_purchase").HasKey(i => i.Id);
        builder.Entity<TourPurchase>().Property(i => i.Id).UseIdentityColumn();

        builder.Entity<TourPurchase>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(tp => tp.TourId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TourPurchase>()
            .HasOne<Purchase>()
            .WithMany()
            .HasForeignKey(tp => tp.PurchaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>().ToTable("comment").HasKey(i => i.Id);
        builder.Entity<Comment>().Property(i => i.Id).UseIdentityColumn();

        builder.Entity<Comment>()
            .HasOne(i => i.Tour)
            .WithMany()
            .HasForeignKey(tp => tp.TourId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(i => i.User)
            .WithMany()
            .HasForeignKey(tp => tp.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
