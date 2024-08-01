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
                new Urun { UrunId = 1, UrunIsim = "Masaüstü Bilgisayar", Adet = 16, Fiyat = 26600},
                new Urun { UrunId = 2, UrunIsim = "Çevre Bileşenleri", Adet = 17, Fiyat = 14850},
                new Urun { UrunId = 3, UrunIsim = "Ofis Mobilyaları", Adet = 20, Fiyat = 18840},
            };
        }

        public Task UrunEkleAsync(Urun urun)
        {
            if (_urunler.Any(x => x.UrunIsim.Equals(urun.UrunIsim, StringComparison.OrdinalIgnoreCase)))
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
            if (_urunler.Any(x => x.UrunId != urun.UrunId &&
            x.UrunIsim.ToLower() == urun.UrunIsim.ToLower())) return Task.CompletedTask;

            var urn = _urunler.FirstOrDefault(x => x.UrunId == urun.UrunId);
            if (urn is not null)
            {
                urn.UrunIsim = urun.UrunIsim;
                urn.Adet = urun.Adet;
                urn.Fiyat = urun.Fiyat;
                urn.UrunEnvanterleri = urun.UrunEnvanterleri;
            }
            return Task.CompletedTask;
        }

        public async Task<Urun> IDdenUrunBulAsync(int urunID)
        {
            var urn = _urunler.FirstOrDefault(x => x.UrunId == urunID);
            var yeniUrun = new Urun();
            if (urn != null)
            {
                yeniUrun.UrunId = urn.UrunId;
                yeniUrun.UrunIsim = urn.UrunIsim;
                yeniUrun.Adet = urn.Adet;
                yeniUrun.Fiyat = urn.Fiyat;
                yeniUrun.UrunEnvanterleri = new List<UrunEnvanter>();
                if (urn.UrunEnvanterleri != null && urn.UrunEnvanterleri.Count > 0)
                {
                    foreach (var urunEnvanter in urn.UrunEnvanterleri)
                    {
                        var yeniUrunEnvanter = new UrunEnvanter
                        {
                            EnvanterId = urunEnvanter.EnvanterId,
                            UrunId = urunEnvanter.UrunId,
                            Urun = urn,
                            Envanter = new Envanter(),
                            EnvanterAdeti = urunEnvanter.EnvanterAdeti
                        };
                        if (urunEnvanter.Envanter != null)
                        {
                            yeniUrunEnvanter.Envanter.EnvanterId = urunEnvanter.Envanter.EnvanterId;
                            yeniUrunEnvanter.Envanter.EnvanterIsim = urunEnvanter.Envanter.EnvanterIsim;
                            yeniUrunEnvanter.Envanter.Adet = urunEnvanter.Envanter.Adet;
                            yeniUrunEnvanter.Envanter.Fiyat = urunEnvanter.Envanter.Fiyat;
                        }
                        yeniUrun.UrunEnvanterleri.Add(yeniUrunEnvanter);
                    }
                }
            }
            return await Task.FromResult(yeniUrun);
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
