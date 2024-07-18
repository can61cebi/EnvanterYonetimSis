using EYS.CoreBusiness;

namespace EYS.UseCases.Urunler
{
    public interface IUrunDuzenleUseCase
    {
        int UrunId { get; set; }

        Task ExecuteAsync(Urun urun);
    }
}