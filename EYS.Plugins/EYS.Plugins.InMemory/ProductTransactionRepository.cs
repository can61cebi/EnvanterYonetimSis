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
                satmaSayisi = satisNumarasi,
                UrunId = urun.UrunId,
                OncekiAdet = urun.Adet,
                SonrakiAdet = urun.Adet - adet,
                IslemZamani = DateTime.Now,
                AlanKisi = yapanKisi,
                AdetFiyati = AdetFiyati
            });

            return Task.CompletedTask;
        }
    }
}
