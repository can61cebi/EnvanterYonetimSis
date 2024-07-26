using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Aksiyonlar
{
    public class EnvanterSatinAlUseCase : IEnvanterSatinAlUseCase
    {
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;
        private readonly IInventoryRepository inventoryRepository;

        public EnvanterSatinAlUseCase(IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository)

        {
            this.inventoryTransactionRepository = inventoryTransactionRepository;
            this.inventoryRepository = inventoryRepository;
        }

        public async Task ExecuteAsync(string almaSayisi, Envanter envanter, int adet, string alanKisi)
        {
            // Satın alma tablosunu girin
            inventoryTransactionRepository.SatinAlAsync(almaSayisi, envanter, adet, alanKisi, envanter.Fiyat);

            // Adeti arttırın
            envanter.Adet += adet;
            await this.inventoryRepository.EnvanterGuncelleAsync(envanter);
        }
    }
}
