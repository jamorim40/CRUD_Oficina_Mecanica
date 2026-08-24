using Mecanica.Services.Interfaces;
using Mecanica.Validations.Interfaces.Veiculo;
using Microsoft.AspNetCore.Mvc;
using Mecanica.Models.Dtos.Requests;
using Mecanica.Models.Dtos.Requests.Veiculo;
using Mecanica.Normalizers;
using Mecanica.Models.Entities;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var veiculo = await _veiculoService.ObterPorId(id);
            if (veiculo is null)
                return NotFound("Veiculo não encontrado ou inativo.");
            return Ok(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CriarVeiculoDto dto)
        {
            dto.Placa = PlacaNormalizer.Normalizar(dto.Placa);
            var erros = _veiculoValidador.validador(dto);
            if (erros.Any())
                return BadRequest(erros);
            await _veiculoService.CriarAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AtualizarVeiculoDto dto)
        {
            dto.Placa = PlacaNormalizer.Normalizar(dto.Placa);
            await _veiculoService.AtualizarAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _veiculoService.SoftDeleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
