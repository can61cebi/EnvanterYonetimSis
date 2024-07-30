using EYS.CoreBusiness;

namespace EYS.UseCases.PluginInterfaces
{
    public interface IProductTransactionRepository
    {
        Task UretAsync(string uretimNumarasi, Urun urun, int adet, string alanKisi);
        Task UrunSatAsync(string SatisNumarasi, Urun urun, int adet, double AdetFiyati, string yapanKisi);
    }
}