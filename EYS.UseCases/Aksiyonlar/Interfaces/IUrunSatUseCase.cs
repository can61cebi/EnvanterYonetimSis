using EYS.CoreBusiness;

namespace EYS.UseCases.Aksiyonlar
{
    public interface IUrunSatUseCase
    {
        Task ExecuteAsync(string satisNumarasi, Urun urun, int adet, double AdetFiyati, string yapanKisi);
    }
}