using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Normalizers;
using Mecanica.Services.Interfaces;
using Mecanica.Validations.Interfaces.Cliente;
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
        public async Task<IActionResult> Get()
        {
            var clientes = await _clienteService.ObterTodos();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clienteService.ObterPorId(id);
            if (cliente is null)
                return NotFound("Cliente não enocntrado ou inativo.");
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CriaClienteDto dto)
        {
            dto.Telefone = TelefoneNormalizado.Normalizar(dto.Telefone);
            dto.Email = EmailNormalizado.Normalizar(dto.Email);

            var erros = _clienteValidador.validador(dto);
            if (erros.Any())
                return BadRequest(erros);

            await _clienteService.CriarAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AtualizarClienteDto dto)
        {
            dto.Telefone = TelefoneNormalizado.Normalizar(dto.Telefone);
            dto.Email = EmailNormalizado.Normalizar(dto.Email);

            await _clienteService.AtualizarAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clienteService.SoftDeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }
        }

    }
}
