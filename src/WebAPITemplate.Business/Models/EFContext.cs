using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace WebAPITemplate.b.Models
{
    public class EFContext : DbContext
    {
        
        public virtual DbSet<NC> NC { get; set; }
        public virtual DbSet<NCXTracciaturaStato> NCXTracciaturaStato { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MasterDatabase"];

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(settings.ConnectionString);
            //optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NC>(entity =>
            {
                entity.ToTable("NC");

                entity.Property(e => e.DataAccadimentoEvento).HasColumnType("datetime");

                entity.Property(e => e.IdOrigine).HasColumnName("IdNCXOrigine");

                entity.Property(e => e.CodiceAudit).HasMaxLength(50);
            });

            modelBuilder.Entity<NCXTracciaturaStato>(entity =>
            {
                entity.ToTable("NCXTracciaturaStato");


                //Per le navigation properties, EF come convenzione usa come Foreign Key (FK) un campo composto da [nome_tabella_esterna]Id
                //quindi abbiamo bisogno di creare questo mapping
                entity.Property(e => e.NCId).HasColumnName("IdNC");
            });

            modelBuilder.Entity<NC>().HasMany(x => x.NCXTracciaturaStato).WithOne();
        }
    }
}
