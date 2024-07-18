using EYS.CoreBusiness;

namespace EYS.UseCases.Urunler
{
    public interface IIsmeGoreUrunleriGoruntuleUseCase
    {
        Task<IEnumerable<Urun>> ExecuteAsync(string name = "");
    }
}