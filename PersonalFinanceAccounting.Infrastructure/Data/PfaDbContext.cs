using Microsoft.EntityFrameworkCore;
using PersonalFinanceAccounting.Infrastructure.Models;

namespace PersonalFinanceAccounting.Infrastructure.Data;

public class PfaDbContext : DbContext
{
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public PfaDbContext()
    {
    }

    public PfaDbContext(DbContextOptions<PfaDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=finance.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(w => w.Currency)
                .HasConversion<string>()
                .HasMaxLength(10);

            entity.Property(w => w.InitialBalance)
                .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            
            entity.Property(t => t.Type)
                .HasConversion<string>()
                .HasMaxLength(20);
            
            entity.Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            entity.Property(t => t.Description)
                .HasMaxLength(500);

            entity.HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId);
        });

    }
}