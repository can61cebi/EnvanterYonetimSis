using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Aksiyonlar
{
    public class UrunUretUseCase : IUrunUretUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;
        private readonly IProductRepository productRepository;
        public UrunUretUseCase(
            IProductTransactionRepository productTransactionRepository,
            IProductRepository productRepository)
        {
            this.productTransactionRepository = productTransactionRepository;
            this.productRepository = productRepository;

        }
        public async Task ExecuteAsync(string uretimNumarasi, Urun urun, int adet, string alanKisi)
        {
            // İşlem kaydı oluştur
            await this.productTransactionRepository.UretAsync(uretimNumarasi, urun, adet, alanKisi);

            // Envanter adetine ekle

            // Ürünün adetini kaydet
            urun.Adet += adet;
            await this.productRepository.UrunGuncelleAsync(urun);
        }
    }
}
