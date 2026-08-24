using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Models.Dtos.Responses.Cliente;
using Mecanica.Models.Entities;

namespace Mecanica.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<RespostaClienteDto>> ObterTodos();
        Task<RespostaClienteDto> ObterPorId(int id);
        Task<Cliente> CriarAsync(CriaClienteDto dto);
        Task<Cliente> AtualizarAsync(int id, AtualizarClienteDto dto);
        Task SoftDeleteAsync(int id);
    }
}
