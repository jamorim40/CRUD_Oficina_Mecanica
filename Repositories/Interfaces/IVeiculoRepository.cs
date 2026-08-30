using Mecanica.Models.Entities;

namespace Mecanica.Repositories.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<List<Veiculo>> ObterTodos();
        Task<Veiculo?> ObterPorId(int id);
        Task<Veiculo?> ObterPorPlaca(string placa);
        Task<Veiculo> CriarVeiculoAsync(Veiculo veiculo);
        Task<Veiculo> AtualizarAsync(Veiculo veiculo);
        Task SoftDeleAsync(int id);
    }
}
