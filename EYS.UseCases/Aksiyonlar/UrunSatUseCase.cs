using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Aksiyonlar
{
    public class UrunSatUseCase : IUrunSatUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;
        private readonly IProductRepository productRepository;

        public UrunSatUseCase(
            IProductTransactionRepository productTransactionRepository,
            IProductRepository productRepository)
        {
            this.productTransactionRepository = productTransactionRepository;
            this.productRepository = productRepository;
        }
        public async Task ExecuteAsync(string SatisNumarasi, Urun urun, int adet, double AdetFiyati, string yapanKisi)
        {
            await this.productTransactionRepository.UrunSatAsync(SatisNumarasi, urun, adet, AdetFiyati, yapanKisi);

            urun.Adet -= adet;
            await this.productRepository.UrunGuncelleAsync(urun);
        }
    }
}
