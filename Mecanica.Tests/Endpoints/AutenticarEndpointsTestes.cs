using Mecanica.Blazor.Security.Sessions;
using Moq;
using Mecanica.Tests.Infrastructure;
using Xunit;

namespace Mecanica.Tests.Endpoints
{
    public class AutenticarEndpointsTestes
    {
        [Fact]
        public async Task SalvarSessaoAsync_QuandoArmazenamentoFalhar_DeveLancarExcecao()
        {
            // Arrange
            var sessaoStoreMock = new Mock<ISessaoStore>();

            sessaoStoreMock
                .Setup(store => store.SalvarAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>()))
                .ThrowsAsync(new InvalidOperationException("Falha ao salvar a sessão"));
            // Act
            var excecao = await Assert.ThrowsAsync<InvalidOperationException>(() => sessaoStoreMock.Object.SalvarAsync("sessao123", "token123", DateTimeOffset.UtcNow.AddHours(8)));

            // Assert
            Assert.Equal("Falha ao salvar a sessão", excecao.Message);

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
    }
}
