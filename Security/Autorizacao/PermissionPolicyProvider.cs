using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Mecanica.Security.Autorizacao
{
    public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private const string Prefix = "Permission";
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }
        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("Permission:"))
            {
                var permissao = policyName.Substring("Permission:".Length);
                var politica = new AuthorizationPolicyBuilder().AddRequirements(new PermissionRequirement(permissao)).Build();
                return politica;
            }
            return await base.GetPolicyAsync(policyName);
        }
    
    }
}
