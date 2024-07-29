using EYS.CoreBusiness;

namespace EYS.UseCases.Aksiyonlar
{
    public interface IUrunUretUseCase
    {
        Task ExecuteAsync(string uretimNumarasi, Urun urun, int adet, string alanKisi);
    }
}