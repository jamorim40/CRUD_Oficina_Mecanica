using Mecanica.Models.Dtos.Requests.Veiculo;
using Mecanica.Models.Dtos.Responses.Veiculo;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mecanica.Services.Service
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _repository;
        public VeiculoService(IVeiculoRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<RespostaVeiculoDto>> ObterTodos()
        {
            var veiculo = await _repository.ObterTodos();
            return veiculo.Select(c => new RespostaVeiculoDto
            {
                Marca = c.Marca,
                Modelo = c.Modelo,
                Palca = c.Placa
            }).ToList();
        }
        public async Task<RespostaVeiculoDto> ObterPorId(int id)
        {
            var veiculo = await _repository.ObterPorId(id);
            if (veiculo is null)
                return null!;
            if (!veiculo.Ativo)
                return null!;
            return new RespostaVeiculoDto
            {
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                Palca = veiculo.Placa
            };
        }       
        public async Task<Veiculo> CriarAsync(CriarVeiculoDto dto)
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

        public async Task<Veiculo> AtualizarAsync(int id, AtualizarVeiculoDto dto)
        {
            var veiculo = await _repository.ObterPorId(id);
            if (veiculo is null)
                throw new Exception("Veiculo não encontrado.");
            if (!veiculo.Ativo)
                throw new Exception("Veiculo inativo.");
            veiculo.Marca = dto.Marca;
            veiculo.Modelo = dto.Modelo;
            veiculo.Placa = dto.Placa;

            return await _repository.AtualizarAsync(veiculo);

        }

        public async Task SoftDeleAsync(int id)
        {
            var veiculo = await _repository.ObterPorId(id);
            if (veiculo is null)
                throw new Exception("Veículo não encontrado. ");
            if (!veiculo.Ativo)
                throw new Exception("Veículo inativo.");
            await _repository.SoftDeleAsync(id);
        }

    }
}
