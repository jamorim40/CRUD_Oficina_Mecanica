using Mecanica.Models.Dtos.Requests.Veiculo;
using Mecanica.Models.Dtos.Responses.Veiculo;
using Mecanica.Models.Entities;

namespace Mecanica.Services.Interfaces
{
    public interface IVeiculoService
    {
        Task<List<VeiculoDtoResponse>> ObterTodos();
        Task<VeiculoDtoResponse> ObterPorId(int id);
        Task<VeiculoDtoResponse> ObterPorPlaca(string placa);
        Task<Veiculo> CriarAsync(CriarVeiculoDtoRequest dto);
        //Task<Veiculo> AtualizarAsync(int id, AtualizarVeiculoDto dto);
        Task<Veiculo> AtualizarAsync(string placa, AtualizarVeiculoDtoRequest dto);
        Task SoftDeleteAsync(string placa);
    }
}
