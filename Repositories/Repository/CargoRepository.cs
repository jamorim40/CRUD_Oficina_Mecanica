using Mecanica.Datas;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mecanica.Repositories.Repository
{
    public class CargoRepository : ICargoRepository
    {
        private readonly AppDbContext _appDbContext;

        public CargoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Cargo>> ObterTodos()
        {
            return await _appDbContext.Cargos.Where(c => c.Ativo).ToListAsync();
            
        }
        public async Task<Cargo?> ObterPorNome(string nome)
        {
            return await _appDbContext.Cargos.FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public async Task<Cargo> CriarAsync(Cargo cargo)
        {
            _appDbContext.Cargos.Add(cargo);
            await _appDbContext.SaveChangesAsync();
            return cargo;          
        }


        public async Task SoftDelete(string nome)
        {
            var cargo = await _appDbContext.Cargos.FirstOrDefaultAsync(c => c.Nome == nome);
            if (cargo is null)
                return;
            cargo.Ativo = false;
            await _appDbContext.SaveChangesAsync();
        }

    }
}
