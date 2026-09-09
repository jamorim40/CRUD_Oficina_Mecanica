using Mecanica.Models.Dtos.Requests.Cargo;
using Mecanica.Models.Dtos.Responses.Cargo;
using Mecanica.Models.Entities;
using Mecanica.Shared;

namespace Mecanica.Services.Interfaces
{
    public interface ICargoService
    {
        Task<List<CargoDtoResponse>> ObterTodos();
        Task<ResultadoServico<CargoDtoResponse>> ObterPorNome(string nome);
        Task<ResultadoServico<CargoDtoResponse>> CriarAsync(CriarCargoDtoRequest dto);
        Task<ResultadoServico<string>> SoftDelete(string nome);
    }
}
