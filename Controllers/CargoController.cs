using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Cargo;
using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Normalizers;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;
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
            var resultado = await _cargoService.ObterPorNome(nome);
            return resultado.ToActionResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CriarCargoDtoRequest dto)
        {
            var resultado = await _cargoService.CriarAsync(dto);
            return resultado.ToActionResult(this);
        }

        [HttpDelete("{nome}")]
        public async Task<IActionResult> Delete(string nome)
        {
            var resultado = await _cargoService.SoftDelete(nome);
            return resultado.ToActionResult(this);
        }
    }
}
