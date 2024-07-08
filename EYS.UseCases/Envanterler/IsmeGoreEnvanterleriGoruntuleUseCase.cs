using EYS.CoreBusiness;
using EYS.UseCases.Envanterler.Interfaces;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Envanterler
{
    public class IsmeGoreEnvanterleriGoruntuleUseCase : IIsmeGoreEnvanterleriGoruntuleUseCase
    {
        private readonly IInventoryRepository inventoryRepository;

        public IsmeGoreEnvanterleriGoruntuleUseCase(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }
        public async Task<IEnumerable<Envanter>> ExecuteAsync(string name = "")
        {
            return await inventoryRepository.IsmeGoreEnvanterleriGoruntuleAsync(name);
        }
    }
}
