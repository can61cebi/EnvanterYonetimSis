using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Raporlar
{
    public class EnvanterAramaIslemleriUseCase : IEnvanterAramaIslemleriUseCase
    {
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;

        public EnvanterAramaIslemleriUseCase(IInventoryTransactionRepository inventoryTransactionRepository)
        {
            this.inventoryTransactionRepository = inventoryTransactionRepository;
        }
        public async Task<IEnumerable<EnvanterIslem>> ExecuteAsync(
            string envanterAdi,
            DateTime? tarihtenItibaren,
            DateTime? tariheKadar,
            EnvanterIslemTipi? islemTipi)
        {
            if(tariheKadar.HasValue) tariheKadar = tariheKadar.Value.AddDays(1);

            return await this.inventoryTransactionRepository.EnvanterIslemleriniGetirAsync(
                envanterAdi,
                tarihtenItibaren,
                tariheKadar,
                islemTipi);
        }
    }
}
