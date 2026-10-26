using Microsoft.AspNetCore.Authentication.Cookies;

namespace Mecanica.Blazor.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthenticationBlazor(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "Mecanica.Auth";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Lax;


                    options.LoginPath = "/login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                });
            return services;
        }
    }
}
