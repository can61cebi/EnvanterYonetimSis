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
    public class UrunEFCoreRepository : IProductRepository
    {
        private readonly IDbContextFactory<EYSIcerik> contextFactory;

        public UrunEFCoreRepository(IDbContextFactory<EYSIcerik> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public async Task<Urun> IDdenUrunBulAsync(int urunID)
        {
            using var db = this.contextFactory.CreateDbContext();
            var urun = await db.Urunler.Include(x => x.UrunEnvanterleri).ThenInclude(x => x.Envanter).FirstOrDefaultAsync(x => x.UrunId == urunID);
            if (urun is not null) return urun;

            return new Urun();
        }

        public async Task IDyeGoreUrunSilAsync(int urunID)
        {
            using var db = this.contextFactory.CreateDbContext();
            var urun = db.Envanterler?.Find(urunID);
            if (urun is null) return;

            db.Envanterler?.Remove(urun);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Urun>> IsmeGoreUrunleriGoruntuleAsync(string name)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.Urunler.Where(x => x.UrunIsim.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public async Task UrunEkleAsync(Urun urun)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.Urunler?.Add(urun);
            EnvanterDegismediDurumu(urun, db);

            await db.SaveChangesAsync();
        }

        public async Task UrunGuncelleAsync(Urun urun)
        {
            using var db = this.contextFactory.CreateDbContext();
            var urn = await db.Urunler.Include(x => x.UrunEnvanterleri).FirstOrDefaultAsync(x => x.UrunId == urun.UrunId);
            if (urn is not null)
            {
                urn.UrunIsim = urun.UrunIsim;
                urn.Fiyat = urn.Fiyat;
                urn.Adet = urun.Adet;
                urun.UrunEnvanterleri = urn.UrunEnvanterleri;
                EnvanterDegismediDurumu(urun, db);

                await db.SaveChangesAsync();
            }
        }

        private void EnvanterDegismediDurumu(Urun urun, EYSIcerik db)
        {
            if (urun?.UrunEnvanterleri is not null && urun.UrunEnvanterleri.Count > 0)
            {
                foreach (var urnEnv in urun.UrunEnvanterleri)
                {
                    if (urnEnv.Envanter is not null)
                    {
                        db.Entry(urnEnv.Envanter).State = EntityState.Unchanged;
                    }
                }
            }
        }
    }
}
