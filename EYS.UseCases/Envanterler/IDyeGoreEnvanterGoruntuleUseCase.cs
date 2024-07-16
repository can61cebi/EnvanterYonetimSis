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
    public class IDyeGoreEnvanterGoruntuleUseCase : IIDyeGoreEnvanterGoruntuleUseCase
    {
        private readonly IInventoryRepository inventoryRepository;
        public IDyeGoreEnvanterGoruntuleUseCase(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public async Task<Envanter> ExecuteAsync(int envanterID)
        {
            return await this.inventoryRepository.IDdenEnvanterBulAsync(envanterID);
        }
    }
}
