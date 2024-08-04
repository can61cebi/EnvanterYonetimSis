using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.Plugins.EFCoreSqlServer
{
    public class InventoryTransactionEFCoreRepository : IInventoryTransactionRepository
    {
        private readonly IDbContextFactory<EYSIcerik> contextFactory;

        public InventoryTransactionEFCoreRepository(IDbContextFactory<EYSIcerik> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<EnvanterIslem>> EnvanterIslemleriniGetirAsync(string envanterAdi, DateTime? tarihtenItibaren, DateTime? tariheKadar, EnvanterIslemTipi? islemTipi)
        {
            using var db = contextFactory.CreateDbContext();

            var sorgu = from ei in db.EnvanterIslemleri
                        join env in db.Envanterler on ei.EnvanterId equals env.EnvanterId
                        where
                        (string.IsNullOrWhiteSpace(envanterAdi) || env.EnvanterIsim.ToLower().IndexOf(envanterAdi.ToLower()) >= 0) &&
                        (!tarihtenItibaren.HasValue || ei.IslemZamani >= tarihtenItibaren.Value) &&
                        (!tariheKadar.HasValue || ei.IslemZamani <= tariheKadar.Value) &&
                        (!islemTipi.HasValue || ei.AksiyonTipi == islemTipi.Value)
                        select ei;

            return await sorgu.Include(x => x.Envanter).ToListAsync();
        }

        public async Task SatinAlAsync(string almaSayisi, Envanter envanter, int adet, string alanKisi, double fiyat)
        {
            using var db = contextFactory.CreateDbContext();

            // Mevcut envanteri bul
            var mevcutEnvanter = await db.Envanterler
                .FirstOrDefaultAsync(e => e.EnvanterIsim == envanter.EnvanterIsim);

            if (mevcutEnvanter != null)
            {
                // Mevcut envanteri güncelle
                mevcutEnvanter.Adet += adet;
                db.Envanterler.Update(mevcutEnvanter);

                // Envanter işlem kaydını ekle
                db.EnvanterIslemleri?.Add(new EnvanterIslem
                {
                    almaSayisi = almaSayisi,
                    OncekiAdet = mevcutEnvanter.Adet - adet,
                    AksiyonTipi = EnvanterIslemTipi.EnvanterSatinAl,
                    SonrakiAdet = mevcutEnvanter.Adet,
                    IslemZamani = DateTime.Now,
                    AlanKisi = alanKisi,
                    EnvanterId = mevcutEnvanter.EnvanterId,
                    AdetFiyati = fiyat
                });
            }
            else
            {
                // Yeni envanteri ekle
                db.Envanterler.Add(envanter);
                await db.SaveChangesAsync();

                // Envanter işlem kaydını ekle
                db.EnvanterIslemleri?.Add(new EnvanterIslem
                {
                    almaSayisi = almaSayisi,
                    OncekiAdet = 0,
                    AksiyonTipi = EnvanterIslemTipi.EnvanterSatinAl,
                    SonrakiAdet = envanter.Adet,
                    IslemZamani = DateTime.Now,
                    AlanKisi = alanKisi,
                    EnvanterId = envanter.EnvanterId,
                    AdetFiyati = fiyat
                });
            }

            await db.SaveChangesAsync();
        }

        public async Task UretAsync(string uretimNumarasi, Envanter envanter, int tuketim, string alanKisi, double fiyat)
        {
            using var db = contextFactory.CreateDbContext();

            db.EnvanterIslemleri?.Add(new EnvanterIslem
            {
                UretimNumarasi = uretimNumarasi,
                EnvanterId = envanter.EnvanterId,
                OncekiAdet = envanter.Adet,
                AksiyonTipi = EnvanterIslemTipi.UrunUret,
                SonrakiAdet = envanter.Adet - tuketim,
                IslemZamani = DateTime.Now,
                AlanKisi = alanKisi,
                AdetFiyati = fiyat
            });

            await db.SaveChangesAsync();
        }
    }
}
