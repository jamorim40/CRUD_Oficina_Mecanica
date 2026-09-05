using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Models.Dtos.Responses.Cliente;
using Mecanica.Models.Entities;

namespace Mecanica.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteDtoResponse>> ObterTodos();
        Task<ClienteDtoResponse> ObterPorId(int id);
        Task<ClienteDtoResponse> ObterPorCpfCnpj(string cpfCnpj);
        Task<Cliente> CriarAsync(CriaClienteDtoRequest dto);
        Task<Cliente> AtualizarAsync(string cpfCnpj, AtualizarClienteDtoRequest dto);
        Task SoftDeleteAsync(string cpfCnpj);
    }
}
