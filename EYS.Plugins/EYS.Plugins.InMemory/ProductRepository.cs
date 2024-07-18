using EYS.CoreBusiness;
using EYS.UseCases.Urunler;
using EYS.UseCases.PluginInterfaces;

namespace EYS.Plugins.InMemory
{
    public class ProductRepository : IProductRepository
    {
        private List<Urun> _urunler;

        public ProductRepository()
        {
            _urunler = new List<Urun>()
            {
                new Urun { UrunId = 1, UrunIsim = "Laptop", Adet = 10, Fiyat = 40000},
                new Urun { UrunId = 2, UrunIsim = "Mouse", Adet = 20, Fiyat = 800},
            };
        }

        public Task UrunEkleAsync(Urun urun)
        {
            if(_urunler.Any(x=> x.UrunIsim.Equals(urun.UrunIsim, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }
            var maxId = _urunler.Max(x => x.UrunId);
            urun.UrunId = maxId + 1;

            _urunler.Add(urun);

            return Task.CompletedTask;
        }

        public Task UrunGuncelleAsync(Urun urun)
        {
            var guncelUrun = _urunler.FirstOrDefault(x => x.UrunId == urun.UrunId);

            if(_urunler.Any(x => x.UrunId != urun.UrunId &&
            x.UrunIsim.Equals(urun.UrunIsim, StringComparison.OrdinalIgnoreCase)))
                { return Task.CompletedTask; }

            if (guncelUrun != null)
            {
                guncelUrun.UrunIsim = urun.UrunIsim;
                guncelUrun.Adet = urun.Adet;
                guncelUrun.Fiyat = urun.Fiyat;
            }

            return Task.CompletedTask;
        }

        public async Task<Urun> IDdenUrunBulAsync(int urunID)
        {
            return await Task.FromResult(_urunler.First(x => x.UrunId == urunID));
        }

        public Task IDyeGoreUrunSilAsync(int urunID)
        {
            var urun = _urunler.FirstOrDefault(x => x.UrunId == urunID);
            if (urun != null)
            {
                _urunler.Remove(urun);
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Urun>> IsmeGoreUrunleriGoruntuleAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_urunler);

            return _urunler.Where(x => x.UrunIsim.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
