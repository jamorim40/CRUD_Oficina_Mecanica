using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Normalizers;
using Mecanica.Services.Interfaces;
using Mecanica.Validations;
using Mecanica.Validations.Interfaces.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IClienteValidation _clienteValidador;

        public ClienteController(
                IClienteService clienteService, 
                IClienteValidation clienteValidador)
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

        [HttpGet("{cpfCnpj}")]
        public async Task<IActionResult> GetById(string cpfCnpj)
        {
            var cliente = await _clienteService.ObterPorCpfCnpj(cpfCnpj);

            if (cliente is null)
                throw new  NaoEncontradoException($"Cliente de cpfCnpj: {cpfCnpj} não encontrado.");

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CriaClienteDtoRequest dto)
        {
            dto.Telefone = TelefoneNormalized.Normalizar(dto.Telefone);
            dto.Email = EmailNormalized.Normalizar(dto.Email);
            //dto.CpfCnpj = DocumentoValidador.ValidarCpfCnpj(dto.CpfCnpj);

            var erros = _clienteValidador.validador(dto);
            if (erros.Any())
                return BadRequest(erros);

            await _clienteService.CriarAsync(dto);
            return Ok();
        }

        [HttpPut("{cpfCnpj}")]
        public async Task<IActionResult> Put(string cpfCnpj, AtualizarClienteDtoRequest dto)
        {
            dto.Telefone = TelefoneNormalized.Normalizar(dto.Telefone);
            dto.Email = EmailNormalized.Normalizar(dto.Email);

            await _clienteService.AtualizarAsync(cpfCnpj, dto);
            return NoContent();
        }

        [HttpDelete("{cpfCnpj}")]
        public async Task<IActionResult> Delete(string cpfCnpj)
        {
            //return Ok(cpfCnpj);
            try
            {

                await _clienteService.SoftDeleteAsync(cpfCnpj);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
