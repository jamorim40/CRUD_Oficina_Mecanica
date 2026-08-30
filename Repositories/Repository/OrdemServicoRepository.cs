using Mecanica.Datas;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mecanica.Repositories.Repository
{
    public class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrdemServicoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<OrdemServico>> ObeterTodos()
        {
            return await _appDbContext.OrdemServicos
                .Include(o => o.Veiculo)
                .Where(o => o.Ativo).ToListAsync();
        }
        public async Task<List<OrdemServico>> ObterPorPlaca(string placa)
        {
            return await _appDbContext
                .OrdemServicos
                .Include(o => o.Veiculo)
                .Where(o => o.Ativo && o.Veiculo!.Placa == placa).ToListAsync();
        }
        public async Task<OrdemServico> CriarAsync(OrdemServico ordemServico)
        {
            _appDbContext.OrdemServicos.Add(ordemServico);
            await _appDbContext.SaveChangesAsync();
            return ordemServico;
        }
        public async Task<OrdemServico?> ObterPorRomaneio(int romaneio)
        {
            return await _appDbContext.OrdemServicos
                .Include(o => o.Veiculo)
                .FirstOrDefaultAsync(o => o.Romaneio == romaneio && o.Ativo);
        }

        public async Task<OrdemServico> AtualizarAsync(OrdemServico ordemServico)
        {
            _appDbContext.OrdemServicos.Update(ordemServico);
            await _appDbContext.SaveChangesAsync();
            return ordemServico;
        }

        public async Task SoftDeleteAsync(int romaneio)
        {

            var ordemServico = await _appDbContext.OrdemServicos
                .FirstOrDefaultAsync(o => o.Romaneio == romaneio);

            ordemServico!.Ativo = false; 

            await _appDbContext.SaveChangesAsync();
            
        }

    }
}
