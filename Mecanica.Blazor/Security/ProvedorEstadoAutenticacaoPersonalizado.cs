using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Mecanica.Blazor.Security
{
    public class ProvedorEstadoAutenticacaoPersonalizado : AuthenticationStateProvider
    {
        private ClaimsPrincipal _usuarioAutenticado = new (new ClaimsIdentity());
       
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_usuarioAutenticado));
        }
       

        public void AutenticarUsuario(string login)
        {
            var reivindicacoes = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login)
            };

            var identidade = new ClaimsIdentity(reivindicacoes, authenticationType: "Blazor");
            _usuarioAutenticado = new ClaimsPrincipal(identidade);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void EncerrarSessao()
        {
            _usuarioAutenticado = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
