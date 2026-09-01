using Mecanica.Models.Entities;

namespace Mecanica.Repositories.Interfaces
{
    public interface IOrdemServicoRepository
    {
        Task<List<OrdemServico>> ObterTodos();
        Task<List<OrdemServico>> ObterPorPlaca(string placa);
        Task<OrdemServico> CriarAsync(OrdemServico ordemServico);
        Task<OrdemServico?> ObterPorRomaneio(int romaneio);
        Task<OrdemServico> AtualizarAsync(OrdemServico ordemServico);
        Task SoftDeleteAsync(int romaneio);
    }
}
