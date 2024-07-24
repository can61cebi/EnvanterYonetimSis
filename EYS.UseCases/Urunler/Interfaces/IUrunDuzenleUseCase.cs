using EYS.CoreBusiness;

namespace EYS.UseCases.Urunler
{
    public interface IUrunDuzenleUseCase
    {
        Task ExecuteAsync(Urun urun);
    }
}