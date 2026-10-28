using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Mecanica.Tests.Infrastructure
{
    public static class FabricaTesteJwt
    {
        public static string GerarTokenValido()
        {
            var token = new JwtSecurityToken
                (
                issuer: "Mecanica.Tests",
                audience: "Mecanica.Blazor",
                expires: DateTime.Now.AddHours(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
