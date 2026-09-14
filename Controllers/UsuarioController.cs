using Mecanica.Models.Dtos.Requests.Usuario;
using Mecanica.Services.Interfaces;
using Mecanica.Services.Service;
using Mecanica.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetById(int matricula)
        {
            var resultado = await _usuarioService.ObterPorMatricula(matricula);
            return resultado.ToActionResult(this);
        }

        [HttpPost("Criar usuário")]
        public async Task<IActionResult> Post(CriarUsuarioDtoRequest dto)
        {
            var resultado = await _usuarioService.CriarAsync(dto);
            return resultado.ToActionResult(this);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Post(LoginUsuarioDtoRequest dto)
        {
            var resultado = await _usuarioService.LoginAsync(dto);
            return resultado.ToActionResult(this);
        }

        [HttpPost("Alterar senha")]
        public async Task<IActionResult> Post(AlterarSenhaPrimeiroAcessoUsuarioDtoRequest dto)
        {
            var resultado = await _usuarioService.AlterarSenhaPrimeiroAcessoAsync(dto);
            return resultado.ToActionResult(this);
        }

        [HttpPost("Resetar senha")]
        public async Task<IActionResult> Post(ResetarUsuarioDtoRequest dto)
        {
            var resultado =await _usuarioService.ResetarUsuarioAsync(dto);
            return resultado.ToActionResult(this);
        }
    }
}
