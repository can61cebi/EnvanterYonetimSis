﻿using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.Urunler
{
    public class UrunEkleUseCase : IUrunEkleUseCase
    {
        private readonly IProductRepository productRepository;

        public UrunEkleUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task ExecuteAsync(Urun urun)
        {
            await this.productRepository.UrunEkleAsync(urun);
        }
    }
}
