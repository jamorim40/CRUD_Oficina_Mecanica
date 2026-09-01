using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Veiculo;
using Mecanica.Normalizers;
using Mecanica.Services.Interfaces;
using Mecanica.Validations.Interfaces.Veiculo;
using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IVeiculoValidador _veiculoValidador;

        public VeiculoController(IVeiculoService veiculoService, IVeiculoValidador veiculoValidador)
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
            var veiculo = await _veiculoService.ObterPorPlaca(placa);
            if (veiculo is null)
                throw new NaoEncontradoException($"Veículo de placa {placa} não encontrado. ");
            return Ok(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CriarVeiculoDto dto)
        {
            dto.Placa = PlacaNormalizado.Normalizar(dto.Placa);
            var erros = _veiculoValidador.validador(dto);
            if (erros.Any())
                return BadRequest(erros);
            await _veiculoService.CriarAsync(dto);
            return Ok();
        }

        [HttpPut("{placa}")]
        public async Task<IActionResult> Put(string placa, AtualizarVeiculoDto dto)
        {
            dto.Placa = PlacaNormalizado.Normalizar(dto.Placa);
            await _veiculoService.AtualizarAsync(placa, dto);
            return NoContent();
        }

        [HttpDelete("{placa}")]
        public async Task<IActionResult> Delete(string placa)
        {
            try
            {
                await _veiculoService.SoftDeleteAsync(placa);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
