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

        public async Task<List<Cliente>> ObterTodos()
        {
            return await _appDbContext.Clientes.
                    Where(c => c.Ativo)
                    .ToListAsync();
        }

        public async Task<Cliente?> ObterPorId(int id)
        {
            return await _appDbContext.Clientes.
                FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> CriarAsync(Cliente cliente)
        {
            _appDbContext.Clientes.Add(cliente);
            await _appDbContext.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> AtualizarAsync(Cliente cliente)
        {

            _appDbContext.Clientes.Update(cliente);
           
            await _appDbContext.SaveChangesAsync();
            return cliente;
            
        }
       
        public async Task SoftDeleteAsync(string cpfCnpj)
        {
            var cliente = await _appDbContext.Clientes.FirstOrDefaultAsync(c => c.CpfCnpj == cpfCnpj);
            if (cliente == null)
                return;
            cliente.Ativo = false;
            await _appDbContext.SaveChangesAsync();
            
        }

        public async Task<Cliente?> ObterPorCpfCnpj(string cpfCnpj)
        {
            return await _appDbContext.Clientes.FirstOrDefaultAsync(c => c.CpfCnpj == cpfCnpj);
        }
    }
}
