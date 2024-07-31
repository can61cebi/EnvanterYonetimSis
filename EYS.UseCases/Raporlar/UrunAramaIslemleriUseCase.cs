using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Raporlar
{
    public class UrunAramaIslemleriUseCase : IUrunAramaIslemleriUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;

        public UrunAramaIslemleriUseCase(IProductTransactionRepository productTransactionRepository)
        {
            this.productTransactionRepository = productTransactionRepository;
        }
        public async Task<IEnumerable<UrunIslem>> ExecuteAsync(
            string urunAdi,
            DateTime? tarihtenItibaren,
            DateTime? tariheKadar,
            UrunIslemTipi? islemTipi)
        {
            if (tariheKadar.HasValue) tariheKadar = tariheKadar.Value.AddDays(1);

            return await this.productTransactionRepository.UrunIslemleriniGetirAsync(
                urunAdi,
                tarihtenItibaren,
                tariheKadar,
                islemTipi);
        }
    }
}
