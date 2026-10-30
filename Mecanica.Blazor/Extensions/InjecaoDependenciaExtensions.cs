using Mecanica.Blazor.Security;
using Mecanica.Blazor.Security.Sessions;
using Mecanica.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Mecanica.Blazor.Extensions
{
    public static class InjecaoDependenciaExtensions
    {
        public static IServiceCollection AddInjecaoDependencias(this IServiceCollection services)
        {
            services.AddScoped<AutenticacaoServices>();
            //services.AddScoped<ProvedorEstadoAutenticacaoPersonalizado>();
            //services.AddScoped<AuthenticationStateProvider>(provedor => 
                //provedor.GetRequiredService<ProvedorEstadoAutenticacaoPersonalizado>());
            services.AddMemoryCache();
            services.AddSingleton<IArmazenamentoSessaoSecurity, ArmazenamentoSessaoSecurity>();
            return services;
        }
    }
}
