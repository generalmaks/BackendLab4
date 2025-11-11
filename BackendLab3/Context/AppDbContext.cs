using BackendLab3.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendLab3.Context;

public partial class AppDbContext : DbContext
{
    private IConfiguration _config;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Currency> Currencies => Set<Currency>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlite(_config.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Code).HasMaxLength(3).IsRequired();
            entity.Property(c => c.Name).HasMaxLength(50).IsRequired();
            entity.Property(c => c.Symbol).HasMaxLength(5).IsRequired();
            entity.HasIndex(c => c.Code).IsUnique();
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).HasMaxLength(50).IsRequired();
            entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
            entity.HasIndex(u => u.Email).IsUnique();

            entity.HasOne(u => u.DefaultCurrency)
                .WithMany()
                .HasForeignKey(u => u.DefaultCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Currency)
                .WithMany()
                .HasForeignKey(e => e.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        });
    }
}
