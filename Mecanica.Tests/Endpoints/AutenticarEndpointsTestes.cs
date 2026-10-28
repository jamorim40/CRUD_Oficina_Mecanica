using Mecanica.Tests.Infrastructure;
using Mecanica.Blazor.Models.Auth;
using System.Net.Http.Json;
using System.Net;
using Moq;

namespace Mecanica.Tests.Endpoints
{
    public class AutenticarEndpointsTestes
    {
        [Fact]
        public async Task Login_QuandoArmazenamentoFalhar_DeveRetornar503()
        {
            // Arrange
            using var fabrica = new FabricaDeTesteMecanicaBlazor();
            using var client = fabrica.CreateClient();

            var loginRequest = new LoginRequest
            {
                Login = "usuario",
                Senha = "senha"
            };

            // Act
            var resposta = await client.PostAsJsonAsync("/auth/login", loginRequest);

            // Assert
            Assert.Equal(HttpStatusCode.ServiceUnavailable, resposta.StatusCode);

            fabrica.SessaoStoreMock.Verify(
                store => store.SalvarAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTimeOffset>()), Times.Once);
            Console.WriteLine("Verificação Moq: SalvarAsync foi chamado exatamente uma vez");

        }

        [Fact]
        public void FabricaDeTesteMecanicaBlazor_DeveInicializarAplicacaoBlazor()
        {
            // Arrange
            using var fabrica = new FabricaDeTesteMecanicaBlazor();

            // Act
            using var client = fabrica.CreateClient();
            // Assert
            Assert.NotNull(fabrica);
        }

        [Fact]
        public async Task Login_QuandoTokenForInvalido_DeveRetornar502()
        {
            // Arrange
            using var fabrica = new FabricaDeTesteMecanicaBlazor
            {
                TokenSimulado = "token_invalido"

            };
            // Simula um token inválido
            using var client = fabrica.CreateClient();

            // Act
            var resposta = await client.PostAsJsonAsync("/auth/login", new LoginRequest
            {
                Login = "usuario",
                Senha = "senha"

            });
            // Assert
            Assert.Equal(HttpStatusCode.BadGateway, resposta.StatusCode);
            fabrica.SessaoStoreMock.Verify(
                store => store.SalvarAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTimeOffset>()),
                Times.Never);
        }
    }
}
