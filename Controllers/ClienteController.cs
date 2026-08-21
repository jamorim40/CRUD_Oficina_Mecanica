using Mecanica.Models.Dtos;
using Mecanica.Normalizers;
using Mecanica.Services.Interfaces;
using Mecanica.Validations;
using Mecanica.Validations.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IClienteValidator _clienteValidador;

        public ClienteController(
                IClienteService clienteService, 
                IClienteValidator clienteValidador)
        {
            _clienteService = clienteService;
            _clienteValidador = clienteValidador;
        }

        [HttpGet]
        public async Task<IActionResult> Get(ClienteCreateDto dto)
        {
            var clientes = await _clienteService.ObterTodos();
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteCreateDto dto)
        {
            dto.Telefone = TelefoneNormalizer.Normalizar(dto.Telefone);
            dto.Email = EmailNormalizer.Normalizar(dto.Email);

            var erros = _clienteValidador.validador(dto);
            if (erros.Any())
                return BadRequest(erros);

            await _clienteService.CriarAsync(dto);
            return Ok();
        }

    }
}
