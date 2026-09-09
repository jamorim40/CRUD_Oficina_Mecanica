using Mecanica.Models.Entities;

namespace Mecanica.Repositories.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<List<Funcionario>> ObterTodos();
        Task<Funcionario?> ObterPorMatricula(int matricula);
        Task<Funcionario> CriarAsync(Funcionario funcionario);
        Task<Funcionario> AtualizarAsync(Funcionario funcionario);
        Task SoftDeleteAsync(int matricula);
        Task<bool> ExisteFuncionarioPorCargo(int cargoId);
    }
}
