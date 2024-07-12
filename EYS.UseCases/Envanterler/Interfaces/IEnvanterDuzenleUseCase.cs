using EYS.CoreBusiness;

namespace EYS.UseCases.Envanterler.Interfaces
{
    public interface IEnvanterDuzenleUseCase
    {
        Task ExecuteAsync(Envanter envanter);
    }
}