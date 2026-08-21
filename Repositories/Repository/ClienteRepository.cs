using Mecanica.Datas;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mecanica.Repositories.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClienteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            _appDbContext.Clientes.Add(cliente);
            await _appDbContext.SaveChangesAsync();
            return cliente;
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _appDbContext.Clientes.
                    Where(c => c.Ativo)
                    .ToListAsync();
        }

        public Task<Cliente> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
