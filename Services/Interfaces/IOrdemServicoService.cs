using Mecanica.Models.Dtos.Requests.OrdemServico;
using Mecanica.Models.Dtos.Responses.OrdemServico;
using Mecanica.Models.Entities;
using Mecanica.Shared;

namespace Mecanica.Services.Interfaces
{
    public interface IOrdemServicoService
    {
        Task<List<CriarOrdemServicoDtoResponse>> ObterTodos();
        Task<List<CriarOrdemServicoDtoResponse>> ObterPorPlaca(string placa);
        Task<ResultadoServico<CriarOrdemServicoDtoResponse>> CriarAsync(CriarOrdemServicoDtoRequest dto);
        Task<ResultadoServico<AtualizarOrdemServicoDtoResponse>> AtualizarAsync(
            int romaneio, 
            AtualizarOrdemServicoDtoRequest dto);
        Task <ResultadoServico<string>>SoftDelete(int romaneio);
    }
}
