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
                new Envanter { EnvanterId = 1, EnvanterIsim = "Ekran Kartı", Adet = 17, Fiyat = 12000},
                new Envanter { EnvanterId = 2, EnvanterIsim = "İşlemci", Adet = 16, Fiyat = 6000},
                new Envanter { EnvanterId = 3, EnvanterIsim = "Anakart", Adet = 16, Fiyat = 4000},
                new Envanter { EnvanterId = 4, EnvanterIsim = "Rastgele Erişim Bellek", Adet = 22, Fiyat = 600},
                new Envanter { EnvanterId = 5, EnvanterIsim = "Güç Kaynağı", Adet = 16, Fiyat = 2000},
                new Envanter { EnvanterId = 6, EnvanterIsim = "Katı Hal Sürücüsü", Adet = 20, Fiyat = 2000},
                new Envanter { EnvanterId = 7, EnvanterIsim = "Monitör", Adet = 28, Fiyat = 13000},
                new Envanter { EnvanterId = 8, EnvanterIsim = "Klavye", Adet = 30, Fiyat = 1100},
                new Envanter { EnvanterId = 9, EnvanterIsim = "Fare", Adet = 28, Fiyat = 750},
                new Envanter { EnvanterId = 10, EnvanterIsim = "Kulaklık", Adet = 17, Fiyat = 1600},
                new Envanter { EnvanterId = 11, EnvanterIsim = "Masa", Adet = 20, Fiyat = 11000},
                new Envanter { EnvanterId = 12, EnvanterIsim = "Koltuk", Adet = 20, Fiyat = 6800},
                new Envanter { EnvanterId = 13, EnvanterIsim = "Raf", Adet = 20, Fiyat = 780},
                new Envanter { EnvanterId = 14, EnvanterIsim = "Priz", Adet = 50, Fiyat = 200},
                new Envanter { EnvanterId = 15, EnvanterIsim = "Kalem", Adet = 60, Fiyat = 50},
                new Envanter { EnvanterId = 16, EnvanterIsim = "Kağıt", Adet = 2000, Fiyat = 10},
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

        public async Task<Envanter> IDdenEnvanterBulAsync(int envanterID)
        {
            return await Task.FromResult(_envanterler.First(x => x.EnvanterId == envanterID));
        }

        public Task IDyeGoreEnvanterSilAsync(int envanterID)
        {
            var envanter = _envanterler.FirstOrDefault(x => x.EnvanterId == envanterID);
            if (envanter != null)
            {
                _envanterler.Remove(envanter);
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Envanter>> IsmeGoreEnvanterleriGoruntuleAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_envanterler);

            return _envanterler.Where(x => x.EnvanterIsim.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
