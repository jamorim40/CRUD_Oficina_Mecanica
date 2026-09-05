using Mecanica.Datas;
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
            return await _appDbContext.Funcionarios.AnyAsync(f => f.CargoId == cargoId && f.Ativo);
        }
    }
}
