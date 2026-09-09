using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Cargo;
using Mecanica.Models.Dtos.Responses.Cargo;
using Mecanica.Models.Dtos.Responses.Cliente;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;

namespace Mecanica.Services.Service
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        public CargoService(ICargoRepository cargoRepository, IFuncionarioRepository funcionarioRepository)
        {
            _cargoRepository = cargoRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<List<CargoDtoResponse>> ObterTodos()
        {
            var cargo = await _cargoRepository.ObterTodos();
            return cargo.Select(c => new CargoDtoResponse
            {
                Nome = c.Nome
            }).ToList();
        }

        public async Task<ResultadoServico<CargoDtoResponse>> ObterPorNome(string nome)
        {
            var cargo = await _cargoRepository.ObterPorNome(nome);
            if (cargo is null)
                return ResultadoServico<CargoDtoResponse>.Falha($"Cargo: {nome} não encontrado.", 404);
            if (!cargo.Ativo)
                return ResultadoServico<CargoDtoResponse>.Falha("Cargo inativo.", 400);

            var dto = new CargoDtoResponse
            {
                Nome = cargo.Nome,
            };

            return ResultadoServico<CargoDtoResponse>.Ok(dto, "Ok", 200);
        }

        public async Task<ResultadoServico<CargoDtoResponse>> CriarAsync(CriarCargoDtoRequest dto)
        {
            var existente = await _cargoRepository.ObterPorNome(dto.Nome);
            if (existente is not null)
            {
                return ResultadoServico<CargoDtoResponse>.Falha($"O cargo '{dto.Nome}' já existe.", 409);
            }

            var cargo = new Cargo
            {
                Nome = dto.Nome,
            };

            var criado = await _cargoRepository.CriarAsync(cargo);
            var resposta = new CargoDtoResponse { Nome = criado.Nome };
            return ResultadoServico<CargoDtoResponse>.Ok(resposta, "Criado", 201);
        }

        public async Task<ResultadoServico<string>> SoftDelete(string nome)
        {
            var cargo = await _cargoRepository.ObterPorNome(nome);

            if (cargo is null)
                return ResultadoServico<string>.Falha($"Cargo {nome} não encontrado.", 404);

            var possuiFuncionarios = await _funcionarioRepository.ExisteFuncionarioPorCargo(cargo.Id);

            if (possuiFuncionarios)
            {
                return ResultadoServico<string>.Falha("Não é possivel excluir um cargo vinculado a funcionários.", 409);
            }

            await _cargoRepository.SoftDelete(nome);
            return ResultadoServico<string>.Ok(null, "Excluído", 204);
        }

    }
}
