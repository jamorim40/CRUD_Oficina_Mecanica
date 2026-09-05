using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Cargo;
using Mecanica.Models.Dtos.Responses.Cargo;
using Mecanica.Models.Dtos.Responses.Cliente;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;

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

        public async Task<CargoDtoResponse> ObterPorNome(string nome)
        {
            var cargo = await _cargoRepository.ObterPorNome(nome);
            if (cargo is null)
                return null!;
            if (!cargo.Ativo)
                return null!;

            return new CargoDtoResponse
            {
                Nome = cargo.Nome,
              
            };
        }

        public async Task<Cargo> CriarAsync(CriarCargoDtoRequest dto)
        {
            var cargo = await _cargoRepository.ObterPorNome(dto.Nome);
            if (cargo is not null)
            {
                throw new RegraNegocioException($" O cargo '{dto.Nome}' já existe.");
            }
           cargo = new Cargo
            {
                Nome = dto.Nome,
            };
                
           return await _cargoRepository.CriarAsync(cargo);
        }


        public async Task SoftDelete(string nome)
        {
            var cargo = await _cargoRepository.ObterPorNome(nome);

            if (cargo is null)
                throw new NaoEncontradoException($"Cargo {nome} não encontrado. ");

            var possuiFuncionarios = await _funcionarioRepository.ExisteFuncionarioPorCargo(cargo.Id);

            if (possuiFuncionarios)
            {
                throw new RegraNegocioException("Não é possivel excluir um cargo vinculado a funcionários.");
            }

            await _cargoRepository.SoftDelete(nome);
        }

    }
}
