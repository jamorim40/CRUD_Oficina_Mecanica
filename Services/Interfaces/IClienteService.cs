using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Models.Dtos.Responses.Cliente;
using Mecanica.Models.Entities;
using Mecanica.Shared;

namespace Mecanica.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteDtoResponse>> ObterTodos();
        Task<ClienteDtoResponse> ObterPorId(int id);
        Task<ResultadoServico<ClienteDtoResponse>> ObterPorCpfCnpj(string cpfCnpj);
        Task<ResultadoServico<ClienteDtoResponse>> CriarAsync(CriaClienteDtoRequest dto);
        Task<ResultadoServico<ClienteDtoResponse>> AtualizarAsync(string cpfCnpj, AtualizarClienteDtoRequest dto);
        Task<ResultadoServico<string>> SoftDeleteAsync(string cpfCnpj);
    }
}
