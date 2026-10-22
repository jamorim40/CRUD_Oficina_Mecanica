using Mecanica.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Mecanica.Security.Autorizacao
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context
            , PermissionRequirement requirement)
        {
            var permissoes = context.User.FindAll(TiposClaimCustom.Permissoes).Select(c => c.Value);
            if (permissoes.Contains(requirement.Permissao))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
