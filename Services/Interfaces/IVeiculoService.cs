using Mecanica.Models.Dtos.Requests.Veiculo;
using Mecanica.Models.Dtos.Responses.Veiculo;
using Mecanica.Models.Entities;

namespace Mecanica.Services.Interfaces
{
    public interface IVeiculoService
    {
        Task<List<RespostaVeiculoDto>> ObterTodos();
        Task<RespostaVeiculoDto> ObterPorId(int id);
        Task<Veiculo> CriarAsync(CriarVeiculoDto dto);
        Task<Veiculo> AtualizarAsync(int id, AtualizarVeiculoDto dto);
        Task SoftDeleAsync(int id);
    }
}
