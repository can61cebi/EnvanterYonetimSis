using EYS.CoreBusiness;

namespace EYS.UseCases.Urunler
{
    public interface IIDyeGoreUrunGoruntuleUseCase
    {
        Task<Urun?> ExecuteAsync(int urunID);
    }
}