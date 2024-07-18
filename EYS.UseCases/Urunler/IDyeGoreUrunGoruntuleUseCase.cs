using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Urunler
{
    public class IDyeGoreUrunGoruntuleUseCase : IIDyeGoreUrunGoruntuleUseCase
    {
        private readonly IProductRepository productRepository;
        public IDyeGoreUrunGoruntuleUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Urun> ExecuteAsync(int urunID)
        {
            return await this.productRepository.IDdenUrunBulAsync(urunID);
        }
    }
}
