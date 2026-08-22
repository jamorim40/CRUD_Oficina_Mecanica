using Mecanica.Models.Dtos.Requests;
using Mecanica.Models.Dtos.Responses;
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
        public async Task<List<RespostaClienteDto>> ObterTodos()
        {
            var cliente = await _repository.ObterTodos();
            return cliente.Select(c => new RespostaClienteDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Telefone = c.Telefone,
            }).ToList();
        }

        public async Task<RespostaClienteDto> ObterPorId(int id)
        {
            var cliente = await _repository.ObterPorId(id);
            if (cliente is null)
                throw new Exception($"Cliente{id} não encontrado");
            if (!cliente.Ativo)
                throw new Exception($"Cliente {id}está inativo");
            return new RespostaClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
            };
        }
        public async Task<Cliente> CriarAsync(CriaClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Email = dto.Email,
            };
            return await _repository.CriarAsync(cliente);
            
        }

        public async Task<Cliente> AtualizarAsync(int id, AtualizarClienteDto dto)
        {
            var cliente = await _repository.ObterPorId(id);
            if (cliente is null)
                throw new Exception("Cliente não encontrado. ");
            if (!cliente.Ativo)
                throw new Exception("Cliente inativo. ");
            cliente.Nome = dto.Nome;
            cliente.Telefone = dto.Telefone;
            cliente.Email = dto.Email;

           return await _repository.AtualizarAsync(cliente);
        }
    }
}
