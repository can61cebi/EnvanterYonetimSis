using EYS.CoreBusiness;
using EYS.UseCases.Envanterler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.PluginInterfaces
{
    public interface IInventoryRepository
    {
        Task EnvanterEkleAsync(EnvanterEkleUseCase envanter);
        Task EnvanterEkleAsync(Envanter envanter);
        Task EnvanterGuncelleAsync(EnvanterDuzenleUseCase envanter);
        Task EnvanterGuncelleAsync(Envanter envanter);
        Task<IEnumerable<Envanter>> IsmeGoreEnvanterleriGoruntuleAsync(string name);
    }
}
