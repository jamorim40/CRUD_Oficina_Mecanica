using Mecanica.Models.Dtos;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;

namespace Mecanica.Services.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Cliente>> ObterTodos()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Cliente> CriarAsync(ClienteCreateDto dto)
        {
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Email = dto.Email,
            };
            return await _repository.AddAsync(cliente);
            
        }
    }
}
