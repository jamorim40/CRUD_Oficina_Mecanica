using Mecanica.Models.Dtos.Requests.Cargo;
using Mecanica.Security.Autorizacao;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;
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

        [RequirePermission("CARGO_CONSULTAR")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cargo = await _cargoService.ObterTodos();
            return Ok(cargo);
        }

        [RequirePermission("CARGO_CONSULTAR")]
        [HttpGet("{nome}")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            var resultado = await _cargoService.ObterPorNome(nome);
            return resultado.ToActionResult(this);
        }

        [RequirePermission("CARGO_CRIAR")]
        [HttpPost]
        public async Task<IActionResult> Post(CriarCargoDtoRequest dto)
        {
            var resultado = await _cargoService.CriarAsync(dto);
            return resultado.ToActionResult(this);
        }

        [RequirePermission("CARGO_APAGAR")]
        [HttpDelete("{nome}")]
        public async Task<IActionResult> Delete(string nome)
        {
            var resultado = await _cargoService.SoftDelete(nome);
            return resultado.ToActionResult(this);
        }
    }
}
