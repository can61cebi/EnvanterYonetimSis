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
    public class EnvanterEFCoreRepository : IInventoryRepository
    {
        private readonly IDbContextFactory<EYSIcerik> contextFactory;

        public EnvanterEFCoreRepository(IDbContextFactory<EYSIcerik> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task EnvanterEkleAsync(Envanter envanter)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.Envanterler?.Add(envanter);
            await db.SaveChangesAsync();
        }

        public async Task EnvanterGuncelleAsync(Envanter envanter)
        {
            using var db = this.contextFactory.CreateDbContext();
            var env = await db.Envanterler.FindAsync(envanter.EnvanterId);
            if (env is not null)
            {
                env.EnvanterIsim = envanter.EnvanterIsim;
                env.Fiyat = env.Fiyat;
                env.Adet = envanter.Adet;

                await db.SaveChangesAsync();
            }
        }

        public async Task<Envanter> IDdenEnvanterBulAsync(int envanterID)
        {
            using var db = this.contextFactory.CreateDbContext();
            var envanter = await db.Envanterler.FindAsync(envanterID);
            if (envanter is not null) return envanter;

            return new Envanter();
        }

        public async Task IDyeGoreEnvanterSilAsync(int envanterID)
        {
            using var db = this.contextFactory.CreateDbContext();
            var envanter = db.Envanterler?.Find(envanterID);
            if (envanter is null) return;

            db.Envanterler?.Remove(envanter);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Envanter>> IsmeGoreEnvanterleriGoruntuleAsync(string name)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.Envanterler.Where(x => x.EnvanterIsim.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }
    }
}
