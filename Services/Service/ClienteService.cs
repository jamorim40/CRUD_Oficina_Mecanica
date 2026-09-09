using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Models.Dtos.Responses.Cliente;
using Mecanica.Models.Entities;
using Mecanica.Normalizers;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;

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
        public async Task<ResultadoServico<ClienteDtoResponse>> ObterPorCpfCnpj(string cpfCnpj)
        {
            var cliente = await _repository.ObterPorCpfCnpj(cpfCnpj);
            if (cliente is null)
                return ResultadoServico<ClienteDtoResponse>.Falha($"Cliente de cpfCnpj: {cpfCnpj} não encontrado.", 404);
            if (!cliente.Ativo)
                return ResultadoServico<ClienteDtoResponse>.Falha("Cliente inativo.", 400);

            var dto = new ClienteDtoResponse
            {
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Email = cliente.Email,
                CpfCnpj = cliente.CpfCnpj!
            };

            return ResultadoServico<ClienteDtoResponse>.Ok(dto, "Ok", 200);
        }
        public async Task<ResultadoServico<ClienteDtoResponse>> CriarAsync(CriaClienteDtoRequest dto)
        {
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Email = dto.Email,
                CpfCnpj = dto.CpfCnpj,
            };
            var criado = await _repository.CriarAsync(cliente);

            var respostaDto = new ClienteDtoResponse
            {
                Nome = criado.Nome,
                Telefone = criado.Telefone,
                Email = criado.Email,
                CpfCnpj = criado.CpfCnpj!
            };

            return ResultadoServico<ClienteDtoResponse>.Ok(respostaDto, "Criado", 201);
            
        }
        public async Task<ResultadoServico<ClienteDtoResponse>> AtualizarAsync(string cpfCnpj, AtualizarClienteDtoRequest dto)
        {
            var cliente = await _repository.ObterPorCpfCnpj(cpfCnpj);
            if (cliente is null)
                return ResultadoServico<ClienteDtoResponse>.Falha("Cliente não encontrado.", 404);
            if (!cliente.Ativo)
                return ResultadoServico<ClienteDtoResponse>.Falha("Cliente inativo.", 400);

            cliente.Nome = dto.Nome;
            cliente.Telefone = dto.Telefone;
            cliente.Email = dto.Email;
            cliente.CpfCnpj = dto.CpfCnpj;

           var atualizado = await _repository.AtualizarAsync(cliente);

           var resposta = new ClienteDtoResponse
           {
               Nome = atualizado.Nome,
               Telefone = atualizado.Telefone,
               Email = atualizado.Email,
               CpfCnpj = atualizado.CpfCnpj!
           };

           return ResultadoServico<ClienteDtoResponse>.Ok(resposta, "Atualizado", 200);
        }

       

        public async Task<ResultadoServico<string>> SoftDeleteAsync(string cpfCnpj)
        {
            cpfCnpj = DocumentoNormalized.Normalizar(cpfCnpj);
            var cliente = await _repository.ObterPorCpfCnpj(cpfCnpj);
            if (cliente is null)
                return ResultadoServico<string>.Falha($"Cliente não encontrado. {cpfCnpj}", 404);
            if (!cliente.Ativo)
                return ResultadoServico<string>.Falha("Cliente está inativo", 400);
            await _repository.SoftDeleteAsync(cpfCnpj);
            return ResultadoServico<string>.Ok(null, "Excluído", 204);
        }
    }

}
