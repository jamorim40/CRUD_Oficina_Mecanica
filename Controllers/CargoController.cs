using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Cargo;
using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Normalizers;
using Mecanica.Services.Interfaces;
using Mecanica.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly ICargoService _cargoService;
        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cargo = await _cargoService.ObterTodos();
            return Ok(cargo);
        }

        [HttpGet("{nome}")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            var cargo = await _cargoService.ObterPorNome(nome);

            if (cargo is null)
                throw new NaoEncontradoException($"Cargo: {nome} não encontrado.");

            return Ok(cargo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CriarCargoDtoRequest dto)
        {
            await _cargoService.CriarAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("{nome}")]
        public async Task<IActionResult> Delete(string nome)
        {
            await _cargoService.SoftDelete(nome);
            return NoContent();
        }
    }
}
