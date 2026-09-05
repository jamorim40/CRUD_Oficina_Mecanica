using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Veiculo;
using Mecanica.Models.Dtos.Responses.Veiculo;
using Mecanica.Models.Entities;
using Mecanica.Normalizers;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;

namespace Mecanica.Services.Service
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _repository;
        public VeiculoService(IVeiculoRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<VeiculoDtoResponse>> ObterTodos()
        {
            var veiculo = await _repository.ObterTodos();
            return veiculo.Select(c => new VeiculoDtoResponse
            {
                Marca = c.Marca,
                Modelo = c.Modelo,
                Placa = c.Placa
            }).ToList();
        }
        public async Task<VeiculoDtoResponse> ObterPorId(int id)
        {
            var veiculo = await _repository.ObterPorId(id);
            if (veiculo is null)
                return null!;
            if (!veiculo.Ativo)
                return null!;
            return new VeiculoDtoResponse
            {
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                Placa = veiculo.Placa
            };
        }

        public async Task<VeiculoDtoResponse> ObterPorPlaca(string placa)
        {
            placa = PlacaNormalizado.Normalizar(placa);
            var veiculo = await _repository.ObterPorPlaca(placa);

            if (veiculo is null)
                return null!;
            if (!veiculo.Ativo)
                return null!;

            return new VeiculoDtoResponse
            {
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Placa = veiculo.Placa

            };
        }

        public async Task<Veiculo> CriarAsync(CriarVeiculoDtoRequest dto)
        {
            var veiculo = new Veiculo()
            {
                ClienteId = dto.ClienteId,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Placa = dto.Placa
            };
            return await _repository.CriarVeiculoAsync(veiculo);
        }

        //public async Task<Veiculo> AtualizarAsync(int id, AtualizarVeiculoDto dto)
        //{
        //    var veiculo = await _repository.ObterPorId(id);
        //    if (veiculo is null)
        //        throw new Exception("Veiculo não encontrado.");
        //    if (!veiculo.Ativo)
        //        throw new Exception("Veiculo inativo.");
        //    veiculo.Marca = dto.Marca;
        //    veiculo.Modelo = dto.Modelo;
        //    veiculo.Placa = dto.Placa;

        //    return await _repository.AtualizarAsync(veiculo);

        //}
        public async Task<Veiculo> AtualizarAsync(string placa, AtualizarVeiculoDtoRequest dto)
        {
            var veiculo = await _repository.ObterPorPlaca(placa);
            if (veiculo is null)
                throw new NaoEncontradoException("Veículo não encontrado.");

            var veiculoEncontrado = veiculo;
            if (!veiculoEncontrado.Ativo)
                throw new Exception("Veículo inativo");
            veiculoEncontrado.Marca = dto.Marca;
            veiculoEncontrado.Modelo = dto.Modelo;
            veiculoEncontrado.Placa = dto.Placa;
            return await _repository.AtualizarAsync(veiculoEncontrado);
            //throw new NotImplementedException();
        }

        public async Task SoftDeleteAsync(string placa)
        {
            var veiculo = await _repository.ObterPorPlaca(placa);
            if (veiculo is null)
                throw new Exception("Veículo não encontrado. ");
            if (!veiculo.Ativo)
                throw new Exception("Veículo inativo.");
            await _repository.SoftDeleteAsync(placa);
        }


    }
}
