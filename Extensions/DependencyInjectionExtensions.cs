using Mecanica.Repositories.Interfaces;
using Mecanica.Repositories.Repository;
using Mecanica.Services.Interfaces;
using Mecanica.Services.Service;
using Mecanica.Validations.Interfaces.Cliente;
using Mecanica.Validations.Interfaces.OrdemServico;
using Mecanica.Validations.Interfaces.Veiculo;

namespace Mecanica.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInjecaoDependencias(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteValidation, ClienteValidation>();
            
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IVeiculoValidation, VeiculoValidation>();
           
            services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
            services.AddScoped<IOrdemServicoService, OrdemServicoService>();
            services.AddScoped<IOrdemServicoValidation, OrdemServicoValidation>();
            
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<ICargoService, CargoService>();
            
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            return services;
        }
    }
}
