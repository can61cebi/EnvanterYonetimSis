using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Urunler
{
    public class IsmeGoreUrunleriGoruntuleUseCase : IIsmeGoreUrunleriGoruntuleUseCase
    {
        private readonly IProductRepository productRepository;

        public IsmeGoreUrunleriGoruntuleUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IEnumerable<Urun>> ExecuteAsync(string name = "")
        {
            return await productRepository.IsmeGoreUrunleriGoruntuleAsync(name);
        }
    }
}
