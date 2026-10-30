using Mecanica.Blazor.Security.Sessions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Mecanica.Blazor.Security
{
    public class AutenticacaoSessaoSecurity
        : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IArmazenamentoSessaoSecurity _armazenamentoSessao;

        public AutenticacaoSessaoSecurity(
            IOptionsMonitor<AuthenticationSchemeOptions> opcoes,
            ILoggerFactory registradorLogs,
            UrlEncoder codificador,
            IArmazenamentoSessaoSecurity armazenamentoSessao)
            : base(opcoes, registradorLogs, codificador)
        {
            _armazenamentoSessao = armazenamentoSessao;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Cookies.TryGetValue("Mecanica.Sessao", out var identificadorSessao)
                || string.IsNullOrWhiteSpace(identificadorSessao))
            {
                return AuthenticateResult.NoResult();
            }

            JwtSecurityToken jwt;

            var token = await _armazenamentoSessao.ObterTokenAsync(identificadorSessao);

            if (string.IsNullOrWhiteSpace(token))
            {
                return AuthenticateResult.NoResult();
            }

            try
            {
                var manipuladorToken = new JwtSecurityTokenHandler();
                jwt = manipuladorToken.ReadJwtToken(token);
            }
            catch (ArgumentException)
            {
                return AuthenticateResult.Fail("Token da sessão inválido.");
            }


            if (jwt.ValidTo <= DateTime.UtcNow)
            {
                return AuthenticateResult.Fail("Token da sessão expirado.");
            }

            var login = jwt.Claims.FirstOrDefault(reivindicacao => reivindicacao.Type == "login")?.Value;

            if (string.IsNullOrWhiteSpace(login))
            {
                return AuthenticateResult.Fail("Login não encontrado na sessão.");
            }

            var reivindicacoes = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login)
            };

            var identidade = new ClaimsIdentity(
                reivindicacoes,
                authenticationType: "MecanicaSessao"
            );

            var usuarioAutenticado = new ClaimsPrincipal(identidade);

            var bilheteAutenticacao = new AuthenticationTicket(usuarioAutenticado,"MecanicaSessao");

            return AuthenticateResult.Success(bilheteAutenticacao);
        }
    }
}