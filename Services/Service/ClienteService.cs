using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Models.Dtos.Responses.Cliente;
using Mecanica.Models.Entities;
using Mecanica.Normalizers;
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
        public async Task<List<ClienteDtoResponse>> ObterTodos()
        {
            var cliente = await _repository.ObterTodos();
            return cliente.Select(c => new ClienteDtoResponse
            {
                Nome = c.Nome,
                Telefone = c.Telefone,
                Email = c.Email,
                CpfCnpj = c.CpfCnpj,
            }).ToList();
        }

        public async Task<ClienteDtoResponse> ObterPorId(int id)
        {
            var cliente = await _repository.ObterPorId(id);
            if (cliente is null)
                return null!;
            if (!cliente.Ativo)
                return null!;
            return new ClienteDtoResponse
            {
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Email = cliente.Email
            };
        }
        public async Task<ClienteDtoResponse> ObterPorCpfCnpj(string cpfCnpj)
        {
            var cliente = await _repository.ObterPorCpfCnpj(cpfCnpj);
            if (cliente is null)
                return null!;
            if (!cliente.Ativo)
                return null!;

            return new ClienteDtoResponse
            {
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Email = cliente.Email,
                CpfCnpj = cliente.CpfCnpj!
            };
        }
        public async Task<Cliente> CriarAsync(CriaClienteDtoRequest dto)
        {
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Email = dto.Email,
                CpfCnpj = dto.CpfCnpj,
            };
            return await _repository.CriarAsync(cliente);
            
        }

        public async Task<Cliente> AtualizarAsync(string cpfCnpj, AtualizarClienteDtoRequest dto)
        {
            var cliente = await _repository.ObterPorCpfCnpj(cpfCnpj);
            if (cliente is null)
                throw new Exception("Cliente não encontrado. ");
            if (!cliente.Ativo)
                throw new Exception("Cliente inativo. ");
            cliente.Nome = dto.Nome;
            cliente.Telefone = dto.Telefone;
            cliente.Email = dto.Email;
            cliente.CpfCnpj = dto.CpfCnpj;

           return await _repository.AtualizarAsync(cliente);
        }

       

        public async Task SoftDeleteAsync(string cpfCnpj)
        {
            cpfCnpj = DocumentoNormalized.Normalizar(cpfCnpj);
            var cliente = await _repository.ObterPorCpfCnpj(cpfCnpj);
            if (cliente is null)
                throw new Exception($"Cliente não encontrado. {cpfCnpj} ");
            if (!cliente.Ativo)
                throw new Exception("Cliente está inativo");
            await _repository.SoftDeleteAsync(cpfCnpj);
        }
    }

}
