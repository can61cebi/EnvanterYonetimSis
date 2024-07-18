using EYS.CoreBusiness;

namespace EYS.UseCases.Urunler
{
    public interface IUrunEkleUseCase
    {
        Task ExecuteAsync(Urun urun);
    }
}