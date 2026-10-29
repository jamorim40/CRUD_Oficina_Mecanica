using Mecanica.Blazor.Models.Auth;
using Mecanica.Blazor.Services;
using Mecanica.Blazor.Security.Sessions;
using System.IdentityModel.Tokens.Jwt;

namespace Mecanica.Blazor.Endpoints
{
    public static class AutenticarEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/auth/login", async (
                LoginRequest loginRequest, 
                AutenticacaoServices authService, 
                ISessaoStore sessaoStore,
                HttpContext httpContext) =>
            {
                var resultado = await authService.LoginAsync(loginRequest);
                if (resultado is null)
                    return Results.Problem("Falha na comunicação com a API.", statusCode: 502);

                if (!resultado.Sucesso)
                {
                    return Results.Json(
                        new { mensagem = resultado.Mensagem },
                        statusCode: StatusCodes.Status401Unauthorized);
                }
                JwtSecurityToken jwt;
                var sessaoId = Convert.ToHexString(System.Security.Cryptography.RandomNumberGenerator.GetBytes(32));

                try
                {
                    var manipuladorToken = new JwtSecurityTokenHandler();
                    jwt = manipuladorToken.ReadJwtToken(resultado.Token);


                }
                catch (ArgumentException)
                {
                    return Results.Problem("A API retornou um token inválido. ", statusCode: StatusCodes.Status502BadGateway);
                }

                var expiracao = new DateTimeOffset(jwt.ValidTo, TimeSpan.Zero);
                if (expiracao <= DateTimeOffset.UtcNow)
                {
                    return Results.Problem("O token JWT retornado pela API expirou.", statusCode: StatusCodes.Status502BadGateway);

                }

                try
                {
                    await sessaoStore.SalvarAsync(sessaoId, resultado.Token, expiracao);

                }
                catch (Exception)
                {
                    return Results.Problem("Não foi possível estabelecer a sessão.", statusCode: StatusCodes.Status503ServiceUnavailable);
                }

                httpContext.Response.Cookies.Append(
                    "Mecanica.Sessao",
                    sessaoId,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Path = "/",
                        Expires = expiracao.UtcDateTime
                    });
                return Results.Ok("Credenciais válidas.");
            });

            return endpoints;
        }
    }
}
