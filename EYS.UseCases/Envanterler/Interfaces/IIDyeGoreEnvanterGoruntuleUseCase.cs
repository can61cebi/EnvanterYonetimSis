using EYS.CoreBusiness;

namespace EYS.UseCases.Envanterler.Interfaces
{
    public interface IIDyeGoreEnvanterGoruntuleUseCase
    {
        Task<Envanter> ExecuteAsync(int envanterID);
    }
}