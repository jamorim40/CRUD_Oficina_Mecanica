using Mecanica.Exceptions;
using Mecanica.Models.Dtos.Requests.OrdemServico;
using Mecanica.Normalizers;
using Mecanica.Services.Interfaces;
using Mecanica.Validations.Interfaces.OrdemServico;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ordemServico = await _ordemServicoService.ObterTodos();
            return Ok(ordemServico);
        }

        [HttpGet("{placa}")]
        public async Task<IActionResult> GetByPlaca(string placa)
        {
            var ordemServico = await _ordemServicoService.ObterPorPlaca(placa);

            if (!ordemServico.Any())
                //return NotFound($"Nenhuma ordem de serviço encontrada para a placa {placa} ");
                throw new NaoEncontradoException($"Não vou encontrado Ordem de Servico para a placa: {placa}.");

            return Ok(ordemServico);

        }

        [HttpPost]
        public async Task<IActionResult> Post(CriarOrdemServicoDtoRequest dto)
        {
            var resultado = await _ordemServicoService.CriarAsync(dto);
            if (!resultado.Sucesso)
                return BadRequest(resultado.Mensagem);
            return Ok(resultado.Conteudo);
        }

        [HttpPut("{romaneio}")]
        public async Task<IActionResult> Put(int romaneio, AtualizarOrdemServicoDtoRequest dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Status))
            {
                dto.Status = StatusNormalizado.Normalizar(dto.Status);
            }

            var resultado = await _ordemServicoService.AtualizarAsync(romaneio, dto);

            if (!resultado.Sucesso)
                return NotFound(resultado.Mensagem);

            return Ok(resultado.Conteudo);
        }

        [HttpDelete("{romaneio}")]

        public async Task<IActionResult> Delete(int romaneio)
        {
            var resultado = await _ordemServicoService.SoftDelete(romaneio);

            if (!resultado.Sucesso)
                return NotFound(resultado.Mensagem);
            return Ok(resultado.Conteudo);
        }
    }

}
