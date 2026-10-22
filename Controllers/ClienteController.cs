using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Normalizers;
using Mecanica.Security.Autorizacao;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;
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

        [RequirePermission("CLIENTE_CONSULTAR")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _clienteService.ObterTodos();
            return Ok(clientes);
        }

        [RequirePermission("CLIENTE_CONSULTAR")]
        [HttpGet("{cpfCnpj}")]
        public async Task<IActionResult> GetById(string cpfCnpj)
        {
            var cliente = await _clienteService.ObterPorCpfCnpj(cpfCnpj);

            if (cliente is null)
                throw new  NaoEncontradoException($"Cliente de cpfCnpj: {cpfCnpj} não encontrado.");

            return Ok(cliente);
        }

        [RequirePermission("CLIENTE_CRIAR")]
        [HttpPost]
        public async Task<IActionResult> Post(CriaClienteDtoRequest dtoCriar)
        {
            dtoCriar.Nome = NomeNormalizado.NormalizarNome(dtoCriar.Nome);
            dtoCriar.Telefone = TelefoneNormalized.Normalizar(dtoCriar.Telefone);
            dtoCriar.Email = EmailNormalized.Normalizar(dtoCriar.Email);
            // Normaliza o documento antes de validar (ex.: 508.970.500-37 -> 50897050037)
            dtoCriar.CpfCnpj = DocumentoNormalized.Normalizar(dtoCriar.CpfCnpj);

            var erros = _clienteValidador.validador(dtoCriar);
            if (erros.Any())
                return BadRequest(erros);

            var resultado = await _clienteService.CriarAsync(dtoCriar);
            return resultado.ToActionResult(this);
        }

        [RequirePermission("CLIENTE_EDITAR")]
        [HttpPut("{cpfCnpj}")]
        public async Task<IActionResult> Put(string cpfCnpj, AtualizarClienteDtoRequest dtoAtualizar)
        {
            dtoAtualizar.Nome = NomeNormalizado.NormalizarNome(dtoAtualizar.Nome);
            dtoAtualizar.Telefone = TelefoneNormalized.Normalizar(dtoAtualizar.Telefone);
            dtoAtualizar.Email = EmailNormalized.Normalizar(dtoAtualizar.Email);
            dtoAtualizar.CpfCnpj = DocumentoNormalized.Normalizar(dtoAtualizar.CpfCnpj);
            var erros = _clienteValidador.validador(new CriaClienteDtoRequest
            {
                Nome = dtoAtualizar.Nome,
                Telefone = dtoAtualizar.Telefone,
                Email = dtoAtualizar.Email,
                CpfCnpj = dtoAtualizar.CpfCnpj
            });


            var resultado = await _clienteService.AtualizarAsync(cpfCnpj, dtoAtualizar);
            return resultado.ToActionResult(this);
        }

        [RequirePermission("CLIENTE_APAGAR")]
        [HttpDelete("{cpfCnpj}")]
        public async Task<IActionResult> Delete(string cpfCnpj)
        {
            var resultado = await _clienteService.SoftDeleteAsync(cpfCnpj);
            return resultado.ToActionResult(this);
        }

    }
}
