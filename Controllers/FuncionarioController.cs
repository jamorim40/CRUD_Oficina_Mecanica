using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.Funcionario;
using Mecanica.Normalizers;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;
using Mecanica.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController (IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var funcionario = await _funcionarioService.ObterTodos();
            return Ok(funcionario);
        }

        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetById(int matricula)
        {
            var resultado = await _funcionarioService.ObterPorMatricula(matricula);
            return resultado.ToActionResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CriarFuncionarioDtoRequest dto)
        {
            dto.Telefone = TelefoneNormalized.Normalizar(dto.Telefone);
            dto.Email = EmailNormalized.Normalizar(dto.Email);
            dto.CpfCnpj = DocumentoNormalized.Normalizar(dto.CpfCnpj);

            if (!DocumentoValidation.ValidarCpfCnpj(dto.CpfCnpj))
                return BadRequest("Cpf/Cnpj inválido.");
            
            var resultado = await _funcionarioService.CriarAsync(dto);
            return resultado.ToActionResult(this);
        }

        [HttpPut("{matricula}")]
        public async Task<IActionResult> Put(int matricula, AtualizarFuncionarioDtoRequest dto)
        {
            dto.Telefone = TelefoneNormalized.Normalizar(dto.Telefone);
            dto.Email = EmailNormalized.Normalizar(dto.Email);
            dto.CpfCnpj = DocumentoNormalized.Normalizar(dto.CpfCnpj);

            if (!DocumentoValidation.ValidarCpfCnpj(dto.CpfCnpj))
                return BadRequest("Cpf/Cnpj inválido.");

            var resultado = await _funcionarioService.AtualizarAsync(matricula, dto);
            return resultado.ToActionResult(this);
        }

        [HttpDelete("{matricula}")]
        public async Task<IActionResult> Delete(int matricula)
        {
            var resultado = await _funcionarioService.SoftDeleteAsync(matricula);
            return resultado.ToActionResult(this);
        }
    }
}
