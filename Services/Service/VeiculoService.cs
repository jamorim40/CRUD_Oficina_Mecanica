using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Veiculo;
using Mecanica.Models.Dtos.Responses.Veiculo;
using Mecanica.Models.Entities;
using Mecanica.Normalizers;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;

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

        public async Task<ResultadoServico<VeiculoDtoResponse>> ObterPorPlaca(string placa)
        {
            placa = PlacaNormalizado.Normalizar(placa);
            var veiculo = await _repository.ObterPorPlaca(placa);
            if (veiculo is null)
                return ResultadoServico<VeiculoDtoResponse>.Falha($"Veículo de placa {placa} não encontrado.", 404);
            if (!veiculo.Ativo)
                return ResultadoServico<VeiculoDtoResponse>.Falha("Veículo inativo.", 400);

            var dto = new VeiculoDtoResponse
            {
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Placa = veiculo.Placa
            };

            return ResultadoServico<VeiculoDtoResponse>.Ok(dto, "Ok", 200);
        }

        public async Task<ResultadoServico<VeiculoDtoResponse>> CriarAsync(CriarVeiculoDtoRequest dto)
        {
            var veiculo = new Veiculo()
            {
                ClienteId = dto.ClienteId,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Placa = dto.Placa
            };
            var criado = await _repository.CriarVeiculoAsync(veiculo);

            var resposta = new VeiculoDtoResponse
            {
                Marca = criado.Marca,
                Modelo = criado.Modelo,
                Placa = criado.Placa
            };

            return ResultadoServico<VeiculoDtoResponse>.Ok(resposta, "Criado", 201);
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
        public async Task<ResultadoServico<VeiculoDtoResponse>> AtualizarAsync(string placa, AtualizarVeiculoDtoRequest dto)
        {
            var veiculo = await _repository.ObterPorPlaca(placa);
            if (veiculo is null)
                return ResultadoServico<VeiculoDtoResponse>.Falha("Veículo não encontrado.", 404);

            var veiculoEncontrado = veiculo;
            if (!veiculoEncontrado.Ativo)
                return ResultadoServico<VeiculoDtoResponse>.Falha("Veículo inativo.", 400);

            veiculoEncontrado.Marca = dto.Marca;
            veiculoEncontrado.Modelo = dto.Modelo;
            veiculoEncontrado.Placa = dto.Placa;

            var atualizado = await _repository.AtualizarAsync(veiculoEncontrado);

            var resposta = new VeiculoDtoResponse
            {
                Marca = atualizado.Marca,
                Modelo = atualizado.Modelo,
                Placa = atualizado.Placa
            };

            return ResultadoServico<VeiculoDtoResponse>.Ok(resposta, "Atualizado", 200);
            //throw new NotImplementedException();
        }

        public async Task<ResultadoServico<string>> SoftDeleteAsync(string placa)
        {
            var veiculo = await _repository.ObterPorPlaca(placa);
            if (veiculo is null)
                return ResultadoServico<string>.Falha("Veículo não encontrado.", 404);
            if (!veiculo.Ativo)
                return ResultadoServico<string>.Falha("Veículo inativo.", 400);
            await _repository.SoftDeleteAsync(placa);
            return ResultadoServico<string>.Ok(null, "Excluído", 204);
        }


    }
}
