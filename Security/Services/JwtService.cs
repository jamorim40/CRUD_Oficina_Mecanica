using Mecanica.Models.Entities;
using Mecanica.Security.Interfaces;
using Mecanica.Security.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mecanica.Security.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtsettings;
       public JwtService(IOptions<JwtSettings> jwtsettings)
        {
            _jwtsettings = jwtsettings.Value;
        }

        public string GerarToken(Usuario usuario)
        {
            var tokenManipulado = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtsettings.Key);
           
            var reivindicacoes = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim("login", usuario.Login?? string.Empty),
                new Claim("funcionarioId", usuario.FuncionarioId.ToString()),
                new Claim(ClaimTypes.Name, usuario.Login?? string.Empty)
            };
            if (usuario.Funcionario != null)
            {
                reivindicacoes.Add(new Claim("matricula", usuario.Funcionario.Matricula.ToString()));
            }
            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(reivindicacoes),
                Expires = DateTime.UtcNow.AddHours(_jwtsettings.ExpireHours),
                Issuer = _jwtsettings.Issuer,
                Audience = _jwtsettings.Audience,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenManipulado.CreateToken(tokenDescricao);
            return tokenManipulado.WriteToken(token);


        }
    }
}
