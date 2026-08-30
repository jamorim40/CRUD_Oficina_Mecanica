using Mecanica.Models.Dtos.Requests.OrdemServico;
using Mecanica.Models.Dtos.Responses.OrdemServico;
using Mecanica.Models.Entities;
using Mecanica.Shared;

namespace Mecanica.Services.Interfaces
{
    public interface IOrdemServicoService
    {
        Task<List<RespostaCriarOrdemServicoDto>> ObterTodos();
        Task<List<RespostaCriarOrdemServicoDto>> ObterPorPlaca(string placa);
        Task<ResultadoServico<RespostaCriarOrdemServicoDto>> CriarAsync(RequisicaoCriarOrdemServicoDto dto);
        Task<ResultadoServico<RespostaAtualizarOrdemServicoDto>> AtualizarAsync(
            int romaneio, 
            RequisicaoAtualizarOrdemServicoDto dto);
        Task <ResultadoServico<string>>SoftDelete(int romaneio);
    }
}
