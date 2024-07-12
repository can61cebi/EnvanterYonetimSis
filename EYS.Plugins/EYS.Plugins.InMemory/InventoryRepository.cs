using EYS.CoreBusiness;
using EYS.UseCases.Envanterler;
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

        public Task EnvanterEkleAsync(Envanter envanter)
        {
            if(_envanterler.Any(x=> x.EnvanterIsim.Equals(envanter.EnvanterIsim, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }
            var maxId = _envanterler.Max(x => x.EnvanterId);
            envanter.EnvanterId = maxId + 1;

            _envanterler.Add(envanter);

            return Task.CompletedTask;
        }

        public Task EnvanterEkleAsync(EnvanterEkleUseCase envanter)
        {
            throw new NotImplementedException();
        }

        public Task EnvanterGuncelleAsync(Envanter envanter)
        {
            var guncelEnvanter = _envanterler.FirstOrDefault(x => x.EnvanterId == envanter.EnvanterId);

            if(_envanterler.Any(x => x.EnvanterId != envanter.EnvanterId &&
            x.EnvanterIsim.Equals(envanter.EnvanterIsim, StringComparison.OrdinalIgnoreCase)))
                { return Task.CompletedTask; }

            if (guncelEnvanter != null)
            {
                guncelEnvanter.EnvanterIsim = envanter.EnvanterIsim;
                guncelEnvanter.Adet = envanter.Adet;
                guncelEnvanter.Fiyat = envanter.Fiyat;
            }

            return Task.CompletedTask;
        }

        public Task EnvanterGuncelleAsync(EnvanterDuzenleUseCase envanter)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Envanter>> IsmeGoreEnvanterleriGoruntuleAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_envanterler);

            return _envanterler.Where(x => x.EnvanterIsim.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
