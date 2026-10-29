using Mecanica.Blazor.Models.Auth;
using Mecanica.Blazor.Services;
using Mecanica.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace Mecanica.Tests.Services
{
    public class AutenticacaoServicesTests
    {
        [Theory]
        [InlineData(HttpStatusCode.Unauthorized, "Login ou senha incorreta.")]
        [InlineData(HttpStatusCode.Forbidden, "Funcionário bloqueado ou inativo.")]
        [InlineData(HttpStatusCode.Conflict, "Primeiro acesso. É necessário alterar a senha.")]
        public async Task Login_QuandoApiRecusarAutenticacao_DevePreservarMensagem(HttpStatusCode statusCode,string mensagemEsperada)
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddHttpClient("MecanicaApi", client => { client.BaseAddress = new Uri("https://localhost:5001"); })
                .ConfigurePrimaryHttpMessageHandler(() =>
                    new HttpMessageHandlerSimulado(() =>
                        new HttpResponseMessage(statusCode)
                        {
                            Content = JsonContent.Create(new
                            {
                                message = mensagemEsperada
                            })
                        }));

            using var provider = services.BuildServiceProvider();

            var service = new AutenticacaoServices(
                provider.GetRequiredService<IHttpClientFactory>());

            var login = new LoginRequest
            {
                Login = "usuario_teste",
                Senha = "senha_incorreta"
            };

            // Act
            var resultado = await service.LoginAsync(login);

            // Assert
            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
            Assert.Equal(mensagemEsperada, resultado.Mensagem);
        }

        [Fact]
        public async Task Login_QuandoCredenciaisInvalidas_NaoDeveCriarSessaoNemCookie()
        {
            // Arrange
            using var fabrica = new FabricaDeTesteMecanicaBlazor
            {
                StatusCodeSimulado = HttpStatusCode.Unauthorized
            };

            using var client = fabrica.CreateClient();

            var resposta = await client.PostAsJsonAsync(
                "/auth/login",
                new LoginRequest
                {
                    Login = "usuario_teste",
                    Senha = "senha_incorreta"
                });

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, resposta.StatusCode);

            Assert.False(
                resposta.Headers.TryGetValues("Set-Cookie", out _));

            fabrica.SessaoStoreMock.Verify(
                store => store.SalvarAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTimeOffset>()),
                Times.Never);
        }
    }
}