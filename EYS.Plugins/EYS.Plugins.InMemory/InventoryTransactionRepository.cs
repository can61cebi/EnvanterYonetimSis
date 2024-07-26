using EYS.CoreBusiness;
using EYS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYS.Plugins.InMemory
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        public List<EnvanterIslem> _inventoryTransactions = new List<EnvanterIslem>();
        public void SatinAlAsync(string almaSayisi, Envanter envanter, int adet, string alanKisi, double fiyat)
        {
            this._inventoryTransactions.Add(new EnvanterIslem
            {
                almaSayisi = almaSayisi,
                EnvanterId = envanter.EnvanterId,
                OncekiAdet = envanter.Adet,
                AksiyonTipi = EnvanterIslemTipi.EnvanterSatinAl,
                SonrakiAdet = envanter.Adet + adet,
                IslemZamani = DateTime.Now,
                AlanKisi = alanKisi,
                Envanter = envanter,
                AdetFiyati = fiyat
            });
        }
    }
}
