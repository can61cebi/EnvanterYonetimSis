﻿using EYS.CoreBusiness;
using EYS.UseCases.Envanterler.Interfaces;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Envanterler
{
    public class EnvanterEkleUseCase : IEnvanterEkleUseCase
    {
        private readonly IInventoryRepository inventoryRepository;

        public EnvanterEkleUseCase(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }
        public async Task ExecuteAsync(Envanter envanter)
        {
            await this.inventoryRepository.EnvanterEkleAsync(envanter);
        }
    }
}
