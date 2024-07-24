using EYS.CoreBusiness;
using EYS.UseCases.Envanterler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.UseCases.PluginInterfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Urun>> IsmeGoreUrunleriGoruntuleAsync(string name);
        Task UrunEkleAsync(Urun urun);
        Task UrunGuncelleAsync(Urun urun);
        Task<Urun> IDdenUrunBulAsync(int urunID);
        Task IDyeGoreUrunSilAsync(int urunID);
    }
}