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

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode is HttpStatusCode.Unauthorized
                        or HttpStatusCode.Forbidden
                        or HttpStatusCode.Conflict)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine($"JSON recebido: {json}");

                        var erro = System.Text.Json.JsonSerializer
                            .Deserialize<ErroApiResponse>(
                                json,
                                new System.Text.Json.JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                });

                        return new LoginResponse
                        {
                            Sucesso = false,
                            Mensagem = erro?.Mensagem ?? "Não foi possível realizar o login.",
                        };
                    }

                    return null;
                }

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
