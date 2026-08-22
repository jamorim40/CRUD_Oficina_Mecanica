using Mecanica.Models.Dtos.Requests;
using Mecanica.Models.Dtos.Responses;
using Mecanica.Models.Entities;

namespace Mecanica.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<RespostaClienteDto>> ObterTodos();
        Task<RespostaClienteDto> ObterPorId(int id);
        Task<Cliente> CriarAsync(CriaClienteDto dto);
        Task<Cliente> AtualizarAsync(int id, AtualizarClienteDto dto);
    }
}
