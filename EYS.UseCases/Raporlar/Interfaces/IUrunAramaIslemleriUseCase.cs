using EYS.CoreBusiness;

namespace EYS.UseCases.Raporlar
{
    public interface IUrunAramaIslemleriUseCase
    {
        Task<IEnumerable<UrunIslem>> ExecuteAsync(string urunAdi, DateTime? tarihtenItibaren, DateTime? tariheKadar, UrunIslemTipi? islemTipi);
    }
}