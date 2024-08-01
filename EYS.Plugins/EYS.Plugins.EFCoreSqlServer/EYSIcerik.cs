using EYS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace EYS.Plugins.EFCoreSqlServer
{
    public class EYSIcerik : DbContext
    {
        public EYSIcerik(DbContextOptions<EYSIcerik> options): base(options)
        {
            
        }

        public DbSet<Envanter> Envanterler { get; set; }
        public DbSet<Urun>? Urunler { get; set; }
        public DbSet<UrunEnvanter>? UrunEnvanterleri { get; set; }
        public DbSet<UrunIslem>? UrunIslemleri { get; set; }
        public DbSet<EnvanterIslem>? EnvanterIslemleri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrunEnvanter>()
                .HasKey(ue => new { ue.UrunId, ue.EnvanterId });
            modelBuilder.Entity<UrunEnvanter>()
                .HasOne(ue => ue.Urun)
                .WithMany(u => u.UrunEnvanterleri)
                .HasForeignKey(ue => ue.UrunId);
            modelBuilder.Entity<UrunEnvanter>()
                .HasOne(ue => ue.Envanter)
                .WithMany(e => e.UrunEnvanterleri)
                .HasForeignKey(ue => ue.EnvanterId);

            // veri girişi
            modelBuilder.Entity<Envanter>().HasData(
                new Envanter { EnvanterId = 1, EnvanterIsim = "Ekran Kartı", Adet = 17, Fiyat = 12000 },
                new Envanter { EnvanterId = 2, EnvanterIsim = "İşlemci", Adet = 16, Fiyat = 6000 },
                new Envanter { EnvanterId = 3, EnvanterIsim = "Anakart", Adet = 16, Fiyat = 4000 },
                new Envanter { EnvanterId = 4, EnvanterIsim = "Rastgele Erişim Bellek", Adet = 32, Fiyat = 600 },
                new Envanter { EnvanterId = 5, EnvanterIsim = "Güç Kaynağı", Adet = 16, Fiyat = 2000 },
                new Envanter { EnvanterId = 6, EnvanterIsim = "Katı Hal Sürücüsü", Adet = 20, Fiyat = 2000 },
                new Envanter { EnvanterId = 7, EnvanterIsim = "Monitör", Adet = 28, Fiyat = 13000 },
                new Envanter { EnvanterId = 8, EnvanterIsim = "Klavye", Adet = 30, Fiyat = 1100 },
                new Envanter { EnvanterId = 9, EnvanterIsim = "Fare", Adet = 28, Fiyat = 750 },
                new Envanter { EnvanterId = 10, EnvanterIsim = "Kulaklık", Adet = 17, Fiyat = 1600 },
                new Envanter { EnvanterId = 11, EnvanterIsim = "Masa", Adet = 20, Fiyat = 11000 },
                new Envanter { EnvanterId = 12, EnvanterIsim = "Koltuk", Adet = 20, Fiyat = 6800 },
                new Envanter { EnvanterId = 13, EnvanterIsim = "Raf", Adet = 20, Fiyat = 780 },
                new Envanter { EnvanterId = 14, EnvanterIsim = "Priz", Adet = 50, Fiyat = 200 },
                new Envanter { EnvanterId = 15, EnvanterIsim = "Kalem", Adet = 60, Fiyat = 50 },
                new Envanter { EnvanterId = 16, EnvanterIsim = "Kağıt", Adet = 2000, Fiyat = 10 }
            );

            modelBuilder.Entity<Urun>().HasData(
                new Urun { UrunId = 1, UrunIsim = "Masaüstü Bilgisayar", Adet = 16, Fiyat = 26600 },
                new Urun { UrunId = 2, UrunIsim = "Çevre Bileşenleri", Adet = 17, Fiyat = 14850 },
                new Urun { UrunId = 3, UrunIsim = "Ofis Mobilyaları", Adet = 20, Fiyat = 18840 }
            );

            modelBuilder.Entity<UrunEnvanter>().HasData(
                new UrunEnvanter { UrunId = 1, EnvanterId = 1, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 1, EnvanterId = 2, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 1, EnvanterId = 3, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 1, EnvanterId = 4, EnvanterAdeti = 2 },
                new UrunEnvanter { UrunId = 1, EnvanterId = 5, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 1, EnvanterId = 6, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 2, EnvanterId = 7, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 2, EnvanterId = 8, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 2, EnvanterId = 9, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 2, EnvanterId = 10, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 3, EnvanterId = 11, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 3, EnvanterId = 12, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 3, EnvanterId = 13, EnvanterAdeti = 1 },
                new UrunEnvanter { UrunId = 3, EnvanterId = 14, EnvanterAdeti = 2 },
                new UrunEnvanter { UrunId = 3, EnvanterId = 15, EnvanterAdeti = 2 },
                new UrunEnvanter { UrunId = 3, EnvanterId = 16, EnvanterAdeti = 100 }
            );
        }
    }
}
