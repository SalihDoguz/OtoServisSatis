using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using OtoServisSatis.Entities;

namespace OtoServisSatis.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Arac> Araclar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Satis> Satislar { get; set; }
        public DbSet<Servis> Servisler { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-L6R702N\\SQLEXPRESS;Database=OtoServisSatisDb;Trusted_Connection=True;TrustServerCertificate=True;");



            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  Fluent API tanımlaması
            modelBuilder.Entity<Marka>().Property(x => x.Adi).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Entity<Rol>().Property(x => x.Adi).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Entity<Rol>().HasData(new Rol
            {
                Id = 1,
                Adi = "Admin"
            });
            modelBuilder.Entity<Kullanici>().HasData(new Kullanici
            {
                Id = 1,
                Adi = "Admin",
                AktifMi = true,
                EklenmeTarihi = DateTime.Now,
                Email = "admin@otoservissatis.tc",
                KullaniciAdi = "admin",
                Sifre = "123456",
                //  Rol = new Rol { Id = 1, },
                RolId = 1,
                Soyadi = "admin",
                Telefon = "0850"
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}
