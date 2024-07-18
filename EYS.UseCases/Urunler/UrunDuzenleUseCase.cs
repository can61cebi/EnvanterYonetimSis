using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Urunler
{
    public class UrunDuzenleUseCase : IUrunDuzenleUseCase
    {
        private readonly IProductRepository productRepository;

        public UrunDuzenleUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public int UrunId { get; set; }

        public async Task ExecuteAsync(Urun urun)
        {
            await this.productRepository.UrunGuncelleAsync(urun);
        }
    }
}
