using Mecanica.Security.Interfaces;
using Mecanica.Security.Services;
using Mecanica.Security.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mecanica.Extensions
{
    public static class JwtExtesions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddScoped<IJwtService, JwtService>();
            var jwtsettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            if (jwtsettings is null)
                throw new InvalidCastException("Configurações JWT não encontradas. ");
            var key = Encoding.UTF8.GetBytes(jwtsettings.Key);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtsettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtsettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });


            return services;
            
        }
    }
}
