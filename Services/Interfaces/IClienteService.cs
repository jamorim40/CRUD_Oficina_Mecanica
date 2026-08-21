using Mecanica.Models.Dtos;
using Mecanica.Models.Entities;

namespace Mecanica.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObterTodos();
        Task<Cliente> CriarAsync(ClienteCreateDto dto);
    }
}
