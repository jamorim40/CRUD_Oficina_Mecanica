using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.OrdemServico;
using Mecanica.Normalizers;
using Mecanica.Security.Autorizacao;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;
using Mecanica.Validations.Interfaces.OrdemServico;

using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdemServicoController : ControllerBase
    {
        private readonly IOrdemServicoService _ordemServicoService;
        private readonly IOrdemServicoValidation _ordemServicoValidador;

        public OrdemServicoController(IOrdemServicoService ordemServicoService, IOrdemServicoValidation ordemServicoValidador)
        {
            _ordemServicoService = ordemServicoService;
            _ordemServicoValidador = ordemServicoValidador;
        }

        [RequirePermission("OS_CONSULTAR")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ordemServico = await _ordemServicoService.ObterTodos();
            return Ok(ordemServico);
        }

        [RequirePermission("OS_CONSULTAR")]
        [HttpGet("{placa}")]
        public async Task<IActionResult> GetByPlaca(string placa)
        {
            var ordemServico = await _ordemServicoService.ObterPorPlaca(placa);

            if (!ordemServico.Any())
                //return NotFound($"Nenhuma ordem de serviço encontrada para a placa {placa} ");
                throw new NaoEncontradoException($"Não vou encontrado Ordem de Servico para a placa: {placa}.");

            return Ok(ordemServico);

        }

        [RequirePermission("OS_CRIAR")]
        [HttpPost]
        public async Task<IActionResult> Post(CriarOrdemServicoDtoRequest dtoCriar)
        {
            var resultado = await _ordemServicoService.CriarAsync(dtoCriar);
            if (!resultado.Sucesso)
                return BadRequest(resultado.Mensagem);
            return Ok(resultado.Conteudo);
        }

        [RequirePermission("OS_EDITAR")]
        [HttpPut("{romaneio}")]
        public async Task<IActionResult> Put(int romaneio, AtualizarOrdemServicoDtoRequest dtoAtualizar)
        {
            if (!string.IsNullOrWhiteSpace(dtoAtualizar.Status))
            {
                dtoAtualizar.Status = StatusNormalizado.Normalizar(dtoAtualizar.Status);
            }

            var resultado = await _ordemServicoService.AtualizarAsync(romaneio, dtoAtualizar);
            return resultado.ToActionResult(this);
        }

        [RequirePermission("OS_APAGAR")]
        [HttpDelete("{romaneio}")]
        public async Task<IActionResult> Delete(int romaneio)
        {
            var resultado = await _ordemServicoService.SoftDelete(romaneio);
            return resultado.ToActionResult(this);
        }
    }

}
