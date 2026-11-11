using CooperativaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CooperativaApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<Cuenta> Cuentas => Set<Cuenta>();
    public DbSet<Movimiento> Movimientos => Set<Movimiento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuenta>().HasKey(c => c.Id);
        modelBuilder.Entity<Movimiento>().HasKey(m => m.Id);

        modelBuilder.Entity<Cuenta>()
            .Property(c => c.NumeroCuenta)
            .IsRequired()
            .HasMaxLength(20);
        
        modelBuilder.Entity<Cuenta>()
            .HasIndex(c => c.NumeroCuenta)
            .IsUnique();

        modelBuilder.Entity<Cuenta>()
            .Property(c => c.Titular)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Cuenta>()
            .Property(c => c.CedulaTitular)
            .IsRequired()
            .HasMaxLength(20);

        modelBuilder.Entity<Movimiento>()
            .Property(m => m.Monto)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Movimiento>()
            .Property(m => m.Fecha)
            .IsRequired();

        modelBuilder.Entity<Cuenta>()
            .Property(c => c.Saldo)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Movimiento>()
            .HasOne(m => m.Cuenta)
            .WithMany(c => c.Movimientos)
            .HasForeignKey(m => m.CuentaId);
    }
}