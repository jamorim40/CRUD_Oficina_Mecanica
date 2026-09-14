using Mecanica.Datas;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mecanica.Repositories.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _appDbContext;
        public FuncionarioRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> ExisteFuncionarioPorCargo(int cargoId)
        {
            return await _appDbContext
                .Funcionarios
                .AnyAsync(f => f.CargoId == cargoId && f.Ativo);
        }
        public async Task<List<Funcionario>> ObterTodos()
        {
           return await _appDbContext
                .Funcionarios
                .Include(f => f.Cargo)
                .Include(f => f.Usuario)
                .Where(f => f.Ativo)
                .ToListAsync();
        }
        public async Task<Funcionario?> ObterPorMatricula(int matricula)
        {
            return await _appDbContext
                .Funcionarios
                .Include(f => f.Usuario)
                .Include(f => f.Cargo)
                .FirstOrDefaultAsync(f => f.Matricula == matricula);
        }

        public async Task<Funcionario> CriarAsync(Funcionario funcionario)
        {
            _appDbContext.Funcionarios.Add(funcionario);
            await _appDbContext.SaveChangesAsync();
            return funcionario;
        }

        public async Task<Funcionario> AtualizarAsync(Funcionario funcionario)
        {
            _appDbContext.Funcionarios.Update(funcionario);
            await _appDbContext.SaveChangesAsync();
            return  funcionario;
        }

        public async Task SoftDeleteAsync(int matricula)
        {
            var funcionario = await _appDbContext
                .Funcionarios
                .FirstOrDefaultAsync(f => f.Matricula == matricula);
            if (funcionario == null)
                return;
            funcionario.Ativo = false;
            await _appDbContext.SaveChangesAsync();

        }
    }
}
