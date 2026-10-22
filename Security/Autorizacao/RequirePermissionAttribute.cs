using Microsoft.AspNetCore.Authorization;

namespace Mecanica.Security.Autorizacao
{
    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        private const string Prefix = "Permission";
        public RequirePermissionAttribute(string permissao) 
        {
            Policy = $"{Prefix}:{permissao}";
        }
    }
}
