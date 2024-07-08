using EYS.CoreBusiness;

namespace EYS.UseCases.Envanterler.Interfaces
{
    public interface IIsmeGoreEnvanterleriGoruntuleUseCase
    {
        Task<IEnumerable<Envanter>> ExecuteAsync(string name = "");
    }
}