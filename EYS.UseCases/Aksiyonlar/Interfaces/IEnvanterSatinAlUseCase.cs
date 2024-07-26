using EYS.CoreBusiness;

namespace EYS.UseCases.Aksiyonlar
{
    public interface IEnvanterSatinAlUseCase
    {
        Task ExecuteAsync(string almaSayisi, Envanter envanter, int adet, string alanKisi);
    }
}