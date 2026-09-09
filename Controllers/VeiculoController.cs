using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Veiculo;
using Mecanica.Normalizers;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;
using Mecanica.Validations.Interfaces.Veiculo;
using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IVeiculoValidation _veiculoValidador;

        public VeiculoController(IVeiculoService veiculoService, IVeiculoValidation veiculoValidador)
        {
            
            _veiculoService = veiculoService;
            _veiculoValidador = veiculoValidador;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var veiculo = await _veiculoService.ObterTodos();
            return Ok(veiculo);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetbyId(int id)
        //{
        //    var veiculo = await _veiculoService.ObterPorId(id);
        //    if (veiculo is null)
        //        //return NotFound("Veiculo não encontrado ou inativo.");
        //        throw new NaoEncontradoException($"Veículo de Id: {id} não encontrado.");
        //    return Ok(veiculo);
        //}

        [HttpGet("{placa}")]
        public async Task<IActionResult> GetByPlaca(string placa)
        {
            var resultado = await _veiculoService.ObterPorPlaca(placa);
            return resultado.ToActionResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CriarVeiculoDtoRequest dto)
        {
            dto.Placa = PlacaNormalizado.Normalizar(dto.Placa);
            var erros = _veiculoValidador.validador(dto);
            if (erros.Any())
                return BadRequest(erros);
            var resultado = await _veiculoService.CriarAsync(dto);
            return resultado.ToActionResult(this);
        }

        [HttpPut("{placa}")]
        public async Task<IActionResult> Put(string placa, AtualizarVeiculoDtoRequest dto)
        {
            dto.Placa = PlacaNormalizado.Normalizar(dto.Placa);
            var resultado = await _veiculoService.AtualizarAsync(placa, dto);
            return resultado.ToActionResult(this);
        }

        [HttpDelete("{placa}")]
        public async Task<IActionResult> Delete(string placa)
        {
            var resultado = await _veiculoService.SoftDeleteAsync(placa);
            return resultado.ToActionResult(this);
        }
    }
}
