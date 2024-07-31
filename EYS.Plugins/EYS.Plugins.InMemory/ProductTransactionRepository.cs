using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.Plugins.InMemory
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private List<UrunIslem> _urunIslemleri = new List<UrunIslem>();

        private readonly IProductRepository productRepository;
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;
        private readonly IInventoryRepository inventoryRepository;

        public ProductTransactionRepository(
            IProductRepository productRepository,
            IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository)
            {
            this.productRepository = productRepository;
            this.inventoryTransactionRepository = inventoryTransactionRepository;
            this.inventoryRepository = inventoryRepository;
        }

        public async Task UretAsync(string uretimNumarasi, Urun urun, int adet, string alanKisi)
        {
            // Envanter adetine ekle
            var urn = await this.productRepository.IDdenUrunBulAsync(urun.UrunId);
            if (urn != null)
            {
                foreach (var ui in urn.UrunEnvanterleri)
                {
                    if (ui.Envanter is not null)
                    {
                        // Envanteri işlemini ekler
                        this.inventoryTransactionRepository.UretAsync(
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
            this._urunIslemleri.Add(new UrunIslem
            {
                UretimNumarasi = uretimNumarasi,
                UrunId = urun.UrunId,
                OncekiAdet = urun.Adet,
                AksiyonTipi = UrunIslemTipi.UrunUret,
                SonrakiAdet = urun.Adet + adet,
                IslemZamani = DateTime.Now,
                AlanKisi = alanKisi,
            });

            // Envanter işlemi ekle

            // Ürün işlemi ekle
        }

        public Task UrunSatAsync(string satisNumarasi, Urun urun, int adet, double AdetFiyati, string yapanKisi)
        {
            this._urunIslemleri.Add(new UrunIslem
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

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<UrunIslem>> UrunIslemleriniGetirAsync(string urunAdi, DateTime? tarihtenItibaren, DateTime? tariheKadar, UrunIslemTipi? islemTipi)
        {
            var urunler = (await productRepository.IsmeGoreUrunleriGoruntuleAsync(string.Empty)).ToList();

            /* Database kullanıldığı takdirde bu sorgu çalıştırılacak
             * select *
             * from urunislemleri ui
             * join urunler urn on ui.urunid = urn.urunid
             */

            var sorgu = from ui in this._urunIslemleri
                        join urn in urunler on ui.UrunId equals urn.UrunId
                        where
                        (string.IsNullOrWhiteSpace(urunAdi) || urn.UrunIsim.ToLower().IndexOf(urunAdi.ToLower()) >= 0) &&
                        (!tarihtenItibaren.HasValue || ui.IslemZamani >= tarihtenItibaren.Value) &&
                        (!tariheKadar.HasValue || ui.IslemZamani <= tariheKadar.Value) &&
                        (!islemTipi.HasValue || ui.AksiyonTipi == islemTipi.Value)
                        select new UrunIslem
                        {
                            urun = urn,
                            UrunIslemId = ui.UrunIslemId,
                            SNumarasi = ui.SNumarasi,
                            UretimNumarasi = ui.UretimNumarasi,
                            UrunId = ui.UrunId,
                            OncekiAdet = ui.OncekiAdet,
                            AksiyonTipi = ui.AksiyonTipi,
                            SonrakiAdet = ui.SonrakiAdet,
                            IslemZamani = ui.IslemZamani,
                            AlanKisi = ui.AlanKisi,
                            AdetFiyati = ui.AdetFiyati,
                        };

            return sorgu;
        }
    }
}
