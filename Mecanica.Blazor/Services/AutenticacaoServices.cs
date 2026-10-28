using Mecanica.Blazor.Models;
using Mecanica.Blazor.Models.Auth;
using System.Net;


namespace Mecanica.Blazor.Services
{
    public class AutenticacaoServices
    {
        //Criar serviço para consumir a API Mecanica.API para autenticação de usuários
        private readonly IHttpClientFactory _httpClientFactory;
        public AutenticacaoServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("MecanicaApi");
                var response = await client.PostAsJsonAsync(
                    "api/Usuario/login",
                    loginRequest);

                if(response.StatusCode == HttpStatusCode.Conflict)
                {
                    return new LoginResponse
                    {
                        Sucesso = false,
                        Mensagem = "Credenciais inválidas."
                    };
                }

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
