using EYS.CoreBusiness;

namespace EYS.UseCases.PluginInterfaces
{
    public interface IProductTransactionRepository
    {
        Task UretAsync(string uretimNumarasi, Urun urun, int adet, string alanKisi);
    }
}