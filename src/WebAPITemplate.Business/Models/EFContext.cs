using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebAPITemplate.Business.Models;

namespace WebAPITemplate.b.Models
{
    public class EFContext : DbContext
    {
        
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductLine> ProductLine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MainDatabase"];

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(settings.ConnectionString);
            //optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                //Per le navigation properties, EF come convenzione usa come Foreign Key (FK) un campo composto da [nome_tabella_esterna]Id
                //quindi abbiamo bisogno di creare questo mapping
                entity.Property(e => e.ProductLineId).HasColumnName("IdProductLine");
            });

            modelBuilder.Entity<ProductLine>(entity =>
            {
                entity.ToTable("ProductLine");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ProductLine>().HasMany(x => x.Products).WithOne();
        }
    }
}
