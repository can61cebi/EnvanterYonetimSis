using EYS.UseCases.Envanterler.Interfaces;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Envanterler
{
    public class EnvanterSilUseCase : IEnvanterSilUseCase
    {
        private readonly IInventoryRepository inventoryRepository;
        public EnvanterSilUseCase(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }
        public async Task ExecuteAsync(int envanterId)
        {
            await this.inventoryRepository.IDyeGoreEnvanterSilAsync(envanterId);
        }
    }
}
