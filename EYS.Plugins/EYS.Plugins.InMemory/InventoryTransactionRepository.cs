using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EYS.Plugins.InMemory
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly IInventoryRepository inventoryRepository;
        public List<EnvanterIslem> _envanterIslemler = new List<EnvanterIslem>();

        public InventoryTransactionRepository(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<EnvanterIslem>> EnvanterIslemleriniGetirAsync(string envanterAdi, DateTime? tarihtenItibaren, DateTime? tariheKadar, EnvanterIslemTipi? islemTipi)
        {
            var envanterler = (await inventoryRepository.IsmeGoreEnvanterleriGoruntuleAsync(string.Empty)).ToList();

            /* Database kullanıldığı takdirde bu sorgu çalıştırılacak
             * select *
             * from envanterislemleri ei
             * join envanterler env on ei.envanterid = env.envanterid
             */

            var sorgu = from ei in this._envanterIslemler
                        join env in envanterler on ei.EnvanterId equals env.EnvanterId
                        where
                        (string.IsNullOrWhiteSpace(envanterAdi) || env.EnvanterIsim.ToLower().IndexOf(envanterAdi.ToLower()) >= 0) &&
                        (!tarihtenItibaren.HasValue || ei.IslemZamani >= tarihtenItibaren.Value) &&
                        (!tariheKadar.HasValue || ei.IslemZamani <= tariheKadar.Value) &&
                        (!islemTipi.HasValue || ei.AksiyonTipi == islemTipi.Value)
                        select new EnvanterIslem
                        {
                            Envanter = env,
                            EnvanterIslemId = ei.EnvanterIslemId,
                            almaSayisi = ei.almaSayisi,
                            EnvanterId = ei.EnvanterId,
                            OncekiAdet = ei.OncekiAdet,
                            AksiyonTipi = ei.AksiyonTipi,
                            SonrakiAdet = ei.SonrakiAdet,
                            IslemZamani = ei.IslemZamani,
                            AlanKisi = ei.AlanKisi,
                            AdetFiyati = ei.AdetFiyati,
                        };

            return sorgu;
        }

        public Task SatinAlAsync(string almaSayisi, Envanter envanter, int adet, string alanKisi, double fiyat)
        {
            this._envanterIslemler.Add(new EnvanterIslem
            {
                almaSayisi = almaSayisi,
                EnvanterId = envanter.EnvanterId,
                OncekiAdet = envanter.Adet,
                AksiyonTipi = EnvanterIslemTipi.EnvanterSatinAl,
                SonrakiAdet = envanter.Adet + adet,
                IslemZamani = DateTime.Now,
                AlanKisi = alanKisi,
                Envanter = envanter,
                AdetFiyati = fiyat
            });

            return Task.CompletedTask;
        }

        public Task UretAsync(string uretimNumarasi, Envanter envanter, int tuketim, string alanKisi, double fiyat)
        {
            this._envanterIslemler.Add(new EnvanterIslem
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

            return Task.CompletedTask;
        }
    }
}
