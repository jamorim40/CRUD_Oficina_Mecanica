using System.IdentityModel.Tokens.Jwt;


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
