using Mecanica.Exceptions;
using Mecanica.Models.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace Mecanica.Middleware
{
    public class ExcecoesMiddleware
    {
        private readonly RequestDelegate _next;

        public ExcecoesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            try
            {
                await _next(contexto);
            }
            catch (Exception ex)
            {
                await TratarExcecoesAssincrona(contexto, ex);
            }
        }

        private static async Task TratarExcecoesAssincrona(HttpContext contexto, Exception excecao)
        {
            contexto.Response.ContentType = "application/json";
            //contexto.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (excecao is NaoEncontradoException)
            {
                contexto.Response.StatusCode = 404;
            }
            else if (excecao is RequisicaoInvalidaException)
            {
                contexto.Response.StatusCode = 400;
            }
            else if (excecao is RegraNegocioException)
            {
                contexto.Response.StatusCode = 409;
            }
            else
            {
                contexto.Response.StatusCode = 500;
            }

            var resposta = new ErroResponse
            {
                Codigo = contexto.Response.StatusCode,
                Mensagem = excecao.Message,
            };

            var json = JsonSerializer.Serialize(resposta);

            await contexto.Response.WriteAsync(json);
        }
    }
}
