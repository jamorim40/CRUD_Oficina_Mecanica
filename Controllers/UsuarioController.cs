using Mecanica.Models.Dtos.Requests.Usuario;
using Mecanica.Repositories.Interfaces;
using Mecanica.Security.Autorizacao;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _repository;
        public UsuarioController(IUsuarioService usuarioService, IUsuarioRepository repository)
        {
            _usuarioService = usuarioService;
            _repository = repository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Post(LoginUsuarioDtoRequest dto)
        {
            var resultado = await _usuarioService.LoginAsync(dto);
            return resultado.ToActionResult(this);
        }



        //[RequirePermission("USUARIO_OBTER")]
        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetById(int matricula)
        {
            var resultado = await _usuarioService.ObterPorMatricula(matricula);
            return resultado.ToActionResult(this);
        }

        [RequirePermission("USUARIO_CRIAR")]
        [HttpPost("Criar")]
        public async Task<IActionResult> Post(CriarUsuarioDtoRequest dto)
        {
            var resultado = await _usuarioService.CriarAsync(dto);
            return resultado.ToActionResult(this);
        }


        [AllowAnonymous]
        [HttpPost("Alterar-Senha")]
        public async Task<IActionResult> Post(AlterarSenhaPrimeiroAcessoUsuarioDtoRequest dto)
        {
            var resultado = await _usuarioService.AlterarSenhaPrimeiroAcessoAsync(dto);
            return resultado.ToActionResult(this);
        }

        [RequirePermission("USUARIO_RESETAR_SENHA")]
        [HttpPost("Resetar-Senha")]
        public async Task<IActionResult> Post(ResetarUsuarioDtoRequest dto)
        {
            var resultado = await _usuarioService.ResetarUsuarioAsync(dto);
            return resultado.ToActionResult(this);
        }

        [RequirePermission("VEICULO_APAGAR")]
        [HttpDelete("{matricula}")]
        public async Task<IActionResult> Delete(int matricula)
        {
            var resultado = await _usuarioService.SoftDelete(matricula);
            return resultado.ToActionResult(this);
        }

        //esse endpoint é apenas para teste, não deve ser usado em produção
        [HttpGet("Permissoes/{UsuarioId}")]
        public async Task<IActionResult> GetPermissoes(int UsuarioId)
        {
            var permissoes = await _repository.ObterPermissoesAsync(UsuarioId);
            return Ok(permissoes);
        }


    }
}                   
