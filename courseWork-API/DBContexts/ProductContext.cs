using courseWork_API.Model;
using Microsoft.EntityFrameworkCore;


namespace courseWork_API.DBContexts
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Price).HasColumnType("decimal(18,0)");
                entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(50);
            });
        }

    }
}
