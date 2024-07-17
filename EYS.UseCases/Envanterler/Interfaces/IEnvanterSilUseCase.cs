namespace EYS.UseCases.Envanterler.Interfaces
{
    public interface IEnvanterSilUseCase
    {
        Task ExecuteAsync(int envanterId);
    }
}