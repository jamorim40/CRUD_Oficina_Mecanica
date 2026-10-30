using Mecanica.Blazor.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Mecanica.Blazor.Extensions
{
    public static class AutenticacaoExtensions
    {
        public const string EsquemaSessao = "MecanicaSessao";
        public static IServiceCollection AddAuthenticationBlazor(
            this IServiceCollection servicos)
        {
            servicos
                .AddAuthentication(AutenticacaoExtensions.EsquemaSessao)
                .AddScheme<AuthenticationSchemeOptions, AutenticacaoSessaoSecurity>(
                    AutenticacaoExtensions.EsquemaSessao,
                    opcoes => { });

            return servicos;
        }
    }
}
