using Microsoft.AspNetCore.Authorization;

namespace Mecanica.Security.Autorizacao
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permissao { get; }
        public PermissionRequirement(string permissao)
        {
            Permissao = permissao;
        }
    }
}
