using Mecanica.Models.Entities;

namespace Mecanica.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObterTodos();
        Task<Cliente?> ObterPorId(int id);
        Task<Cliente?> ObterPorCpfCnpj(string cpfCnpj);
        Task<Cliente> CriarAsync(Cliente cliente);
        Task<Cliente>AtualizarAsync(Cliente cliente);
        Task SoftDeleteAsync(string cpfCnpj);
    }
}
