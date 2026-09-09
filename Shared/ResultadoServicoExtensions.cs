using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Shared
{
    public static class ResultadoServicoExtensions
    {
        public static IActionResult ToActionResult<T>(this ResultadoServico<T> resultado, ControllerBase controller)
        {
            if (resultado is null)
                return controller.NotFound();

            if (resultado.Sucesso)
            {
                if (resultado.Conteudo == null)
                {
                    if (resultado.StatusCode.HasValue && resultado.StatusCode.Value == 201)
                        return controller.StatusCode(201);
                    if (resultado.StatusCode.HasValue && resultado.StatusCode.Value == 204)
                        return controller.NoContent();
                    return controller.NoContent();
                }
                return controller.Ok(resultado.Conteudo);
            }
            else
            {
                var code = resultado.StatusCode ?? 400;
                var body = new { message = resultado.Mensagem };
                return code switch
                {
                    400 => controller.BadRequest(body),
                    404 => controller.NotFound(body),
                    409 => controller.Conflict(body),
                    _ => controller.StatusCode(code, body),
                };
            }
        }
    }
}
