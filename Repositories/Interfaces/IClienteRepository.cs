using Mecanica.Models.Entities;

namespace Mecanica.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(int id);
        Task<Cliente> AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task SoftDeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
