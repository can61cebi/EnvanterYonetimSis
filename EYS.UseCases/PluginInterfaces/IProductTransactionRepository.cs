using EYS.CoreBusiness;

namespace EYS.UseCases.PluginInterfaces
{
    public interface IProductTransactionRepository
    {
        Task<IEnumerable<UrunIslem>> UrunIslemleriniGetirAsync(string urunAdi, DateTime? tarihtenItibaren, DateTime? tariheKadar, UrunIslemTipi? islemTipi);
        Task UretAsync(string uretimNumarasi, Urun urun, int adet, string alanKisi);
        Task UrunSatAsync(string satisNumarasi, Urun urun, int adet, double AdetFiyati, string yapanKisi);
    }
}