using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;

namespace EYS.Plugins.InMemory
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<Envanter> _envanterler;

        public InventoryRepository()
        {
            _envanterler = new List<Envanter>()
            {
                new Envanter { EnvanterId = 1, EnvanterIsim = "MSI Laptop", Adet = 1, Fiyat = 10000},
                new Envanter { EnvanterId = 2, EnvanterIsim = "HyperX Mouse", Adet = 2, Fiyat = 550},
                new Envanter { EnvanterId = 3, EnvanterIsim = "LG Monitor", Adet = 1, Fiyat = 7000},
                new Envanter { EnvanterId = 4, EnvanterIsim = "Corsair Kulaklik", Adet = 1, Fiyat = 300},
            };
        }
        public async Task<IEnumerable<Envanter>> IsmeGoreEnvanterleriGoruntuleAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_envanterler);

            return _envanterler.Where(x => x.EnvanterIsim.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
