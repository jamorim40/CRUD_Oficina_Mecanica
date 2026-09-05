using Mecanica.Models.Entities;

namespace Mecanica.Repositories.Interfaces
{
    public interface ICargoRepository
    {
        Task<List<Cargo>> ObterTodos();
        Task<Cargo?> ObterPorNome(string nome);
        Task<Cargo> CriarAsync(Cargo cargo);
        Task SoftDelete(string nome);
    }
}
