using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Funcionario;
using Mecanica.Models.Dtos.Responses.Funcionario;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;

namespace Mecanica.Services.Service
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _repository;
        public FuncionarioService(IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FuncionarioDtoResponse>> ObterTodos()
        {
            var funcionario = await _repository.ObterTodos();
            return funcionario.Select(f => new FuncionarioDtoResponse
            {
                Nome = f.Nome,
                CpfCnpj = f.CpfCnpj,
                Telefone = f.Telefone,
                Email = f.Email,
                Matricula = f.Matricula,
                Usuario = f.Usuario?.Login,
                NomeCargo = f.Cargo?.Nome

            }).ToList();
        }
        public async Task<ResultadoServico<FuncionarioDtoResponse>> ObterPorMatricula(int matricula)
        {
            var funcionario = await _repository.ObterPorMatricula(matricula);

            if (funcionario is null)
                return ResultadoServico<FuncionarioDtoResponse>.Falha($"Funcionário de matricula: {matricula} não encontrado.", 404);
            if (!funcionario.Ativo)
                return ResultadoServico<FuncionarioDtoResponse>.Falha("Funcionário está inativo.", 400);

            var dto = new FuncionarioDtoResponse
            {
                Nome = funcionario.Nome,
                CpfCnpj = funcionario.CpfCnpj,
                Telefone = funcionario.Telefone,
                Email = funcionario.Email,
                Matricula = funcionario.Matricula,
                Usuario = funcionario.Usuario?.Login,
                NomeCargo = funcionario.Cargo?.Nome
            };

            return ResultadoServico<FuncionarioDtoResponse>.Ok(dto, "Ok", 200);

        }
        public async Task<ResultadoServico<FuncionarioDtoResponse>> CriarAsync(CriarFuncionarioDtoRequest dto)
        {
            var funcionario = new Funcionario
            {
                Nome = dto.Nome,
                CpfCnpj = dto.CpfCnpj,
                Telefone = dto.Telefone,
                Email = dto.Email,
                CargoId = dto.CargoId,
            };
            var criado = await _repository.CriarAsync(funcionario);

            var resposta = new FuncionarioDtoResponse
            {
                Nome = criado.Nome,
                CpfCnpj = criado.CpfCnpj,
                Telefone = criado.Telefone,
                Email = criado.Email,
                Matricula = criado.Matricula,
                Usuario = criado.Usuario?.Login,
                NomeCargo = criado.Cargo?.Nome
            };

            return ResultadoServico<FuncionarioDtoResponse>.Ok(resposta, "Criado", 201);
        }
        public async Task<ResultadoServico<FuncionarioDtoResponse>> AtualizarAsync(int matricula, AtualizarFuncionarioDtoRequest dto)
        {
            var funcioanrio = await _repository.ObterPorMatricula(matricula);

            if (funcioanrio is null)
                return ResultadoServico<FuncionarioDtoResponse>.Falha($"Funcionário de matricula: {matricula} não encontrado.", 404);
            if (!funcioanrio.Ativo)
                return ResultadoServico<FuncionarioDtoResponse>.Falha("Funcionário está inativo.", 400);

            funcioanrio.Nome = dto.Nome;
            funcioanrio.CpfCnpj = dto.CpfCnpj;
            funcioanrio.Telefone = dto.Telefone;
            funcioanrio.Email = dto.Email;
            funcioanrio.CargoId = dto.CargoId;

            var atualizado = await _repository.AtualizarAsync(funcioanrio);

            var resposta = new FuncionarioDtoResponse
            {
                Nome = atualizado.Nome,
                CpfCnpj = atualizado.CpfCnpj,
                Telefone = atualizado.Telefone,
                Email = atualizado.Email,
                Matricula = atualizado.Matricula,
                Usuario = atualizado.Usuario?.Login,
                NomeCargo = atualizado.Cargo?.Nome
            };

            return ResultadoServico<FuncionarioDtoResponse>.Ok(resposta, "Atualizado", 200);

        }

        public async Task<ResultadoServico<string>> SoftDeleteAsync(int matricula)
        {
            var funcionario = await _repository.ObterPorMatricula(matricula);

            if (funcionario is null)
                return ResultadoServico<string>.Falha($"Funcionário de matricula: {matricula} não encontrado.", 404);

            if (!funcionario.Ativo)
                return ResultadoServico<string>.Falha("Funcionário está inativo.", 400);
            await _repository.SoftDeleteAsync(matricula);
            return ResultadoServico<string>.Ok(null, "Excluído", 204);
        }
    }
}
