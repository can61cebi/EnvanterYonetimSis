using EYS.CoreBusiness;

namespace EYS.UseCases.PluginInterfaces
{
    public interface IInventoryTransactionRepository
    {
        Task<IEnumerable<EnvanterIslem>> EnvanterIslemleriniGetirAsync(string envanterAdi, DateTime? tarihtenItibaren, DateTime? tariheKadar, EnvanterIslemTipi? islemTipi);
        Task SatinAlAsync(string almaSayisi, Envanter envanter, int adet, string alanKisi, double fiyat);

        Task UretAsync(string uretimNumarasi, Envanter envanter, int tuketim, string alanKisi, double fiyat);
    }
}