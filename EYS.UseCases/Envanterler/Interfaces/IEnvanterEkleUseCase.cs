using EYS.CoreBusiness;

namespace EYS.UseCases.Envanterler.Interfaces
{
    public interface IEnvanterEkleUseCase
    {
        Task ExecuteAsync(Envanter envanter);
    }
}