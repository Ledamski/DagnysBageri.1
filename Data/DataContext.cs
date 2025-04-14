using Microsoft.EntityFrameworkCore;
using MormorDagnysBageri.api.Entities;

namespace MormorDagnysBageri.api.Data;

public class DataContext : DbContext

{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierItem> SupplierItems { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierItem>().HasKey(o => new {o.ProductId, o.SupplierId });
    }
}