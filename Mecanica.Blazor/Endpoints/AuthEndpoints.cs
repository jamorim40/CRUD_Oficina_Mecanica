using Mecanica.Blazor.Models.Auth;
using Mecanica.Blazor.Services;

namespace Mecanica.Blazor.Endpoints
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/auth/login", async (LoginRequest loginRequest, AuthService authService) =>
            {
                var resultado = await authService.LoginAsync(loginRequest);
                if(resultado is null)
                    return Results.Problem("Falha na comunicação com a API.", statusCode:502);
                //if (!resultado.Sucesso)
                //    return Results.Unauthorized();
                if (!resultado.Sucesso)
                {
                    return Results.Json(
                        new { mensagem = resultado.Mensagem },
                        statusCode: StatusCodes.Status401Unauthorized);
                }

                return Results.Ok("Credenciais válidas.");
            });
            return endpoints;
        }
    }
}
