using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Urunler
{
    public class UrunSilUseCase : IUrunSilUseCase
    {
        private readonly IProductRepository productRepository;
        public UrunSilUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task ExecuteAsync(int urunId)
        {
            await this.productRepository.IDyeGoreUrunSilAsync(urunId);
        }
    }
}
