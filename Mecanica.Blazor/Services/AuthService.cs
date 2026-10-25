using Mecanica.Blazor.Models;
using Mecanica.Blazor.Models.Auth;

namespace Mecanica.Blazor.Services
{
    public class AuthService
    {
        //Criar serviço para consumir a API Mecanica.API para autenticação de usuários
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("MecanicaApi");

                Console.WriteLine($"BaseAddress: {client.BaseAddress}");

                var response = await client.PostAsJsonAsync(
                    "api/Usuario/login",
                    loginRequest);

                Console.WriteLine($"Status: {response.StatusCode}");

                var conteudo = await response.Content.ReadAsStringAsync();

                Console.WriteLine(conteudo);

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
