using Mecanica.Datas;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mecanica.Repositories.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AppDbContext _appDbContext;

        public VeiculoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Veiculo>> ObterTodos()
        {
            return await _appDbContext.Veiculos.
                Where(c => c.Ativo)
                .ToListAsync();
        }

        public async Task<Veiculo?> ObterPorId(int id)
        {
            return await _appDbContext.Veiculos.
                FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Veiculo?> ObterPorPlaca(string placa)
        {
            return await _appDbContext.Veiculos.FirstOrDefaultAsync(v => v.Placa == placa);
        }

        public async Task<Veiculo> CriarVeiculoAsync(Veiculo veiculo)
        {
            _appDbContext.Add(veiculo);
            await _appDbContext.SaveChangesAsync();
            return veiculo;
        }

        public async Task<Veiculo> AtualizarAsync(Veiculo veiculo)
        {
            _appDbContext.Update(veiculo);
            await _appDbContext.SaveChangesAsync();
            return veiculo;
        } 

        public async Task SoftDeleAsync(int id)
        {
            var veiculo = await _appDbContext.Veiculos.FirstOrDefaultAsync(c => c.Id == id);
            if (veiculo is null)
                return;
            veiculo.Ativo = false;
            await _appDbContext.SaveChangesAsync();
        }

    }
}
