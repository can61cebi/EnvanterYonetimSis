using EYS.CoreBusiness;
using EYS.Plugins.EFCoreSqlServer;
using EYS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.Plugins.InMemory
{
    public class ProductTransactionEFCoreRepository : IProductTransactionRepository
    {
        private readonly IDbContextFactory<EYSIcerik> contextFactory;
        private readonly IProductRepository productRepository;
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;
        private readonly IInventoryRepository inventoryRepository;

        public ProductTransactionEFCoreRepository(
            IDbContextFactory<EYSIcerik> contextFactory,
            IProductRepository productRepository,
            IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository)
            {
            this.contextFactory = contextFactory;
            this.productRepository = productRepository;
            this.inventoryTransactionRepository = inventoryTransactionRepository;
            this.inventoryRepository = inventoryRepository;
        }

        public async Task UretAsync(string uretimNumarasi, Urun urun, int adet, string alanKisi)
        {
            using var db = contextFactory.CreateDbContext();

            // Envanter adetine ekle

            var urn = await this.productRepository.IDdenUrunBulAsync(urun.UrunId);
            if (urn != null)
            {
                foreach (var ui in urn.UrunEnvanterleri)
                {
                    if (ui.Envanter is not null)
                    {
                        // Envanteri işlemini ekler
                        await this.inventoryTransactionRepository.UretAsync(
                        uretimNumarasi,
                        ui.Envanter,
                        ui.EnvanterAdeti * adet,
                        alanKisi, -1);

                        // Envanter adetini düşür
                        var env = await this.inventoryRepository.IDdenEnvanterBulAsync(ui.EnvanterId);
                        env.Adet -= ui.EnvanterAdeti * adet;

                        await this.inventoryRepository.EnvanterGuncelleAsync(env);
                    }   
                }
            }

            // Ürün işlemi ekle
            db.UrunIslemleri?.Add(new UrunIslem
            {
                UretimNumarasi = uretimNumarasi,
                UrunId = urun.UrunId,
                OncekiAdet = urun.Adet,
                AksiyonTipi = UrunIslemTipi.UrunUret,
                SonrakiAdet = urun.Adet + adet,
                IslemZamani = DateTime.Now,
                AlanKisi = alanKisi,
            });

            await db.SaveChangesAsync();

            // Envanter işlemi ekle

            // Ürün işlemi ekle
        }

        public async Task UrunSatAsync(string satisNumarasi, Urun urun, int adet, double AdetFiyati, string yapanKisi)
        {
            using var db = contextFactory.CreateDbContext();

            db.UrunIslemleri?.Add(new UrunIslem
            {
                AksiyonTipi = UrunIslemTipi.UrunSat,
                SNumarasi = satisNumarasi,
                UrunId = urun.UrunId,
                OncekiAdet = urun.Adet,
                SonrakiAdet = urun.Adet - adet,
                IslemZamani = DateTime.Now,
                AlanKisi = yapanKisi,
                AdetFiyati = AdetFiyati
            });

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<UrunIslem>> UrunIslemleriniGetirAsync(string urunAdi, DateTime? tarihtenItibaren, DateTime? tariheKadar, UrunIslemTipi? islemTipi)
        {
            using var db = contextFactory.CreateDbContext();

            /* Database kullanıldığı takdirde bu sorgu çalıştırılacak
             * select *
             * from urunislemleri ui
             * join urunler urn on ui.urunid = urn.urunid
             */

            var sorgu = from ui in db.UrunIslemleri
                        join urn in db.Urunler on ui.UrunId equals urn.UrunId
                        where
                        (string.IsNullOrWhiteSpace(urunAdi) || urn.UrunIsim.ToLower().IndexOf(urunAdi.ToLower()) >= 0) &&
                        (!tarihtenItibaren.HasValue || ui.IslemZamani >= tarihtenItibaren.Value) &&
                        (!tariheKadar.HasValue || ui.IslemZamani <= tariheKadar.Value) &&
                        (!islemTipi.HasValue || ui.AksiyonTipi == islemTipi.Value)
                        select ui;

            return await sorgu.Include(x => x.urun).ToListAsync();
        }
    }
}
