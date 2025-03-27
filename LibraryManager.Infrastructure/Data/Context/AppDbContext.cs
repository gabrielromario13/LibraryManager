using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManager.Infrastructure.Data.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext() { }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        if (Database.IsRelational())
            Database.Migrate();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Penalty> Penalties { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath("..\\LibraryManager.API"))
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Isbn)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Loan>()
            .Property(l => l.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Reservation>()
            .Property(r => r.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Loans)
            .WithOne(l => l.Book)
            .HasForeignKey(l => l.BookId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Reservations)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Reservations)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Loans)
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Loans)
            .WithOne(l => l.Book)
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Penalties)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}