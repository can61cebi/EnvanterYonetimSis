using EYS.CoreBusiness;

namespace EYS.UseCases.Raporlar
{
    public interface IEnvanterAramaIslemleriUseCase
    {
        Task<IEnumerable<EnvanterIslem>> ExecuteAsync(string envanterAdi, DateTime? tarihtenItibaren, DateTime? tariheKadar, EnvanterIslemTipi? islemTipi);
    }
}