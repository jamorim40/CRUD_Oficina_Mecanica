using Mecanica.Models.Dtos.Requests.Veiculo;
using Mecanica.Models.Dtos.Responses.Veiculo;
using Mecanica.Models.Entities;
using Mecanica.Shared;

namespace Mecanica.Services.Interfaces
{
    public interface IVeiculoService
    {
        Task<List<VeiculoDtoResponse>> ObterTodos();
        Task<VeiculoDtoResponse> ObterPorId(int id);
        Task<ResultadoServico<VeiculoDtoResponse>> ObterPorPlaca(string placa);
        Task<ResultadoServico<VeiculoDtoResponse>> CriarAsync(CriarVeiculoDtoRequest dto);
        //Task<Veiculo> AtualizarAsync(int id, AtualizarVeiculoDto dto);
        Task<ResultadoServico<VeiculoDtoResponse>> AtualizarAsync(string placa, AtualizarVeiculoDtoRequest dto);
        Task<ResultadoServico<string>> SoftDeleteAsync(string placa);
    }
}
