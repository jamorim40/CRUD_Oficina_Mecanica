using Mecanica.Models.Dtos.Requests.Cargo;
using Mecanica.Models.Dtos.Responses.Cargo;
using Mecanica.Models.Entities;

namespace Mecanica.Services.Interfaces
{
    public interface ICargoService
    {
        Task<List<CargoDtoResponse>> ObterTodos();
        Task<CargoDtoResponse> ObterPorNome(string nome);
        Task<Cargo> CriarAsync(CriarCargoDtoRequest dto);
        Task SoftDelete(string nome);
    }
}
