using EYS.CoreBusiness;

namespace EYS.UseCases.PluginInterfaces
{
    public interface IInventoryTransactionRepository
    {
        void SatinAlAsync(string almaSayisi, Envanter envanter, int adet, string alanKisi, double fiyat);

        void UretAsync(string uretimNumarasi, Envanter envanter, int tuketim, string alanKisi, double fiyat);

    }
}